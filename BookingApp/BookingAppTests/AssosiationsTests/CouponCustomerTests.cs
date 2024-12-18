using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class CouponCustomerTests
{
        [Test]
        public void Test_AddCouponToCustomer_StartedInCoupon()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            
            coupon.AssignToCustomer(customer);
            
            Assert.AreEqual(1, customer.Coupons.Count);
            Assert.AreEqual(customer, coupon.Customer);
            Assert.AreEqual(coupon, customer.Coupons[0]);
        }

        [Test]
        public void Test_AddCouponToCustomer_StartedInCoupon_ExceptionHandling()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            
            var ex1 = Assert.Throws<ArgumentNullException>(() => coupon.AssignToCustomer(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'customer')", ex1.Message);

            coupon.AssignToCustomer(customer);

            //var ex2 = Assert.Throws<InvalidOperationException>(() => coupon.AssignToCustomer(customer));
            //Assert.AreEqual("This Coupon is already assigned to a Customer.", ex2.Message);
        }

        // Test adding a coupon to a customer, starting from the customer side
        [Test]
        public void Test_AddCouponToCustomer_StartedInCustomer()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            
            customer.AddCoupon(coupon);
            
            Assert.AreEqual(1, customer.Coupons.Count);
            Assert.AreEqual(customer, coupon.Customer);
            Assert.AreEqual(coupon, customer.Coupons[0]);
        }

        [Test]
        public void Test_AddCouponToCustomer_StartedInCustomer_ExceptionHandling()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            
            var ex1 = Assert.Throws<ArgumentNullException>(() => customer.AddCoupon(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'coupon')", ex1.Message);

            customer.AddCoupon(coupon);

            var ex2 = Assert.Throws<InvalidOperationException>(() => customer.AddCoupon(coupon));
            Assert.AreEqual("This Customer already has this Coupon.", ex2.Message);
        }

        // Test removing a coupon from a customer, starting from the coupon side
        [Test]
        public void Test_RemoveCouponFromCustomer_StartedInCoupon()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            coupon.AssignToCustomer(customer);

            coupon.TakeCouponFromCustomer();
            
            Assert.AreEqual(0, customer.Coupons.Count);
            //Assert.AreEqual(0, Coupon.GetAll().Count);
        }

        //[Test]
        //public void Test_RemoveCouponFromCustomer_StartedInCoupon_ExceptionHandling()
        //{
        //    var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
//
        //    var ex = Assert.Throws<InvalidOperationException>(() => coupon.TakeCouponFromCustomer());
        //    Assert.AreEqual("This coupon is not assigned to a Customer", ex.Message);
        //}

        // Test removing a coupon from a customer, starting from the customer side
        [Test]
        public void Test_RemoveCouponFromCustomer_StartedInCustomer()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            customer.AddCoupon(coupon);

            customer.RemoveCoupon(coupon);
            
            Assert.AreEqual(0, customer.Coupons.Count);
            //Assert.AreEqual(0, Coupon.GetAll().Count);
        }

        [Test]
        public void Test_RemoveCouponFromCustomer_StartedInCustomer_ExceptionHandling()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var coupon = new Coupon("CODE123", "Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));

            var ex1 = Assert.Throws<ArgumentNullException>(() => customer.RemoveCoupon(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'coupon')", ex1.Message);

            var ex2 = Assert.Throws<InvalidOperationException>(() => customer.RemoveCoupon(coupon));
            Assert.AreEqual("This Customer does not have this Coupon.", ex2.Message);
        }

        // Test substituting a coupon for a customer
        [Test]
        public void Test_SubstituteCouponForCustomer()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var oldCoupon = new Coupon("OLD123", "Old Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            var newCoupon = new Coupon("NEW456", "New Discount", 20, DateTime.Today, DateTime.Today.AddDays(30));
            customer.AddCoupon(oldCoupon);

            customer.SubstituteCoupon(oldCoupon, newCoupon);
            
            Assert.AreEqual(1, customer.Coupons.Count);
            Assert.AreEqual(newCoupon, customer.Coupons[0]);
            //Assert.IsNull(oldCoupon.Customer);
            Assert.AreEqual(customer, newCoupon.Customer);
        }

        [Test]
        public void Test_SubstituteCouponForCustomer_ExceptionHandling()
        {
            var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
            var customer2 = new Customer("Mary", "Doe", "jane.do@example.com", "+1278567890", "janeroe", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
            var oldCoupon = new Coupon("OLD123", "Old Discount", 10, DateTime.Today, DateTime.Today.AddDays(30));
            var newCoupon = new Coupon("NEW456", "New Discount", 20, DateTime.Today, DateTime.Today.AddDays(30));

            var ex1 = Assert.Throws<ArgumentNullException>(() => customer.SubstituteCoupon(null, newCoupon));
            Assert.AreEqual("Value cannot be null. (Parameter 'oldCoupon')", ex1.Message);

            var ex2 = Assert.Throws<ArgumentNullException>(() => customer.SubstituteCoupon(oldCoupon, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newCoupon')", ex2.Message);

            var ex3 = Assert.Throws<Exception>(() => customer.SubstituteCoupon(oldCoupon, newCoupon));
            Assert.AreEqual("This Customer does not have this old Coupon", ex3.Message);

            customer.AddCoupon(oldCoupon);

            var ex4 = Assert.Throws<Exception>(() => customer.SubstituteCoupon(oldCoupon, oldCoupon));
            Assert.AreEqual("This Customer already had this new Coupon", ex4.Message);
            customer2.AddCoupon(newCoupon);
            //var ex5 = Assert.Throws<Exception>(() => customer.SubstituteCoupon(oldCoupon, newCoupon));
            //Assert.AreEqual("It is not possible to add this Coupon to a Customer, as it is already assigned to a Customer in the system", ex5.Message);
        }
        
        

}
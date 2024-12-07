using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class BookingCustomerTests
{
    // Test adding a booking to a customer, starting from the booking side
    [Test]
    public void Test_AddBookingToCustomer_StartedInBooking()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        booking.AddBookingToCustomer(customer);

        Assert.AreEqual(1, customer.Bookings.Count);
        Assert.AreEqual(customer, booking.Customer);
        Assert.AreEqual(booking, customer.Bookings[0]);
    }

    [Test]
    public void Test_AddBookingToCustomer_StartedInBooking_ExceptionHandling()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.AddBookingToCustomer(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'customer')", ex1.Message);

        booking.AddBookingToCustomer(customer);

        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.AddBookingToCustomer(customer));
        Assert.AreEqual("This Booking is already assigned to a Customer.", ex2.Message);
    }

    // Test adding a booking to a customer, starting from the customer side
    [Test]
    public void Test_AddBookingToCustomer_StartedInCustomer()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        customer.AddBooking(booking);

        Assert.AreEqual(1, customer.Bookings.Count);
        Assert.AreEqual(customer, booking.Customer);
        Assert.AreEqual(booking, customer.Bookings[0]);
    }

    [Test]
    public void Test_AddBookingToCustomer_StartedInCustomer_ExceptionHandling()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        var ex1 = Assert.Throws<ArgumentNullException>(() => customer.AddBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'booking')", ex1.Message);

        customer.AddBooking(booking);

        var ex2 = Assert.Throws<InvalidOperationException>(() => customer.AddBooking(booking));
        Assert.AreEqual("This Customer already has this Booking.", ex2.Message);
    }

    // Test removing a booking from a customer, starting from the booking side
    [Test]
    public void Test_RemoveBookingFromCustomer_StartedInBooking()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        booking.AddBookingToCustomer(customer);
        booking.RemoveBookingFromCustomer();

        Assert.AreEqual(0, customer.Bookings.Count);
        Assert.IsNull(booking.Customer);
    }

    [Test]
    public void Test_RemoveBookingFromCustomer_StartedInBooking_ExceptionHandling()
    {
        var booking = new Booking();

        var ex = Assert.Throws<InvalidOperationException>(() => booking.RemoveBookingFromCustomer());
        Assert.AreEqual("This booking is not assigned to a Customer", ex.Message);
    }

    // Test removing a booking from a customer, starting from the customer side
    [Test]
    public void Test_RemoveBookingFromCustomer_StartedInCustomer()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        customer.AddBooking(booking);
        customer.RemoveBooking(booking);

        Assert.AreEqual(0, customer.Bookings.Count);
        Assert.IsNull(booking.Customer);
    }

    [Test]
    public void Test_RemoveBookingFromCustomer_StartedInCustomer_ExceptionHandling()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var booking = new Booking();

        var ex1 = Assert.Throws<ArgumentNullException>(() => customer.RemoveBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'booking')", ex1.Message);

        var ex2 = Assert.Throws<InvalidOperationException>(() => customer.RemoveBooking(booking));
        Assert.AreEqual("This Customer does not have this Booking.", ex2.Message);
    }

    // Test substituting a booking for a customer
    [Test]
    public void Test_SubstituteBookingForCustomer()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var oldBooking = new Booking();
        var newBooking = new Booking();

        customer.AddBooking(oldBooking);
        customer.SubstituteBooking(oldBooking, newBooking);

        Assert.AreEqual(1, customer.Bookings.Count);
        Assert.AreEqual(newBooking, customer.Bookings[0]);
        Assert.IsNull(oldBooking.Customer);
        Assert.AreEqual(customer, newBooking.Customer);
    }

    [Test]
    public void Test_SubstituteBookingForCustomer_ExceptionHandling()
    {
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var oldBooking = new Booking();
        var newBooking = new Booking();

        var ex1 = Assert.Throws<ArgumentNullException>(() => customer.SubstituteBooking(null, newBooking));
        Assert.AreEqual("Value cannot be null. (Parameter 'oldBooking')", ex1.Message);

        var ex2 = Assert.Throws<ArgumentNullException>(() => customer.SubstituteBooking(oldBooking, null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newBooking')", ex2.Message);

        var ex3 = Assert.Throws<Exception>(() => customer.SubstituteBooking(oldBooking, newBooking));
        Assert.AreEqual("This Customer does not have this old Booking", ex3.Message);

        customer.AddBooking(oldBooking);

        var ex4 = Assert.Throws<Exception>(() => customer.SubstituteBooking(oldBooking, oldBooking));
        Assert.AreEqual("This Customer already had this new Booking", ex4.Message);

        var anotherCustomer = new Customer("Jane", "Smith", "jane.smith@example.com", "+1987654321", "janesmith", "password123", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
        anotherCustomer.AddBooking(newBooking);

        var ex5 = Assert.Throws<Exception>(() => customer.SubstituteBooking(oldBooking, newBooking));
        Assert.AreEqual("It is not possible to add this Booking to a Customer, as it is already assigned to a Customer in the system", ex5.Message);
    }

    // Test change customer in booking
    [Test]
    public void Test_ChangeCustomerInBooking_SuccessfulOperation()
    {
        var customer1 = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var customer2 = new Customer("Jane", "Doe", "jane.doe@example.com", "+1234567890", "janedoe", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
        var booking = new Booking();
        
        booking.AddBookingToCustomer(customer1);
        
        booking.ChangeCustomerInBooking(customer2);
        
        Assert.AreEqual(customer2, booking.Customer);
        Assert.AreEqual(1, customer2.Bookings.Count);
        Assert.AreEqual(booking, customer2.Bookings[0]);
        
        Assert.AreEqual(0, customer1.Bookings.Count);
    }
    
    [Test]
    public void Test_ChangeCustomerInBooking_ExceptionHandling()
    {
        var customer1 = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var customer2 = new Customer("Jane", "Doe", "jane.doe@example.com", "+1234567890", "janedoe", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
        var booking = new Booking();
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.ChangeCustomerInBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newCustomer')", ex1.Message);

        booking.AddBookingToCustomer(customer1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.ChangeCustomerInBooking(customer1));
        Assert.AreEqual("This Booking already belongs to exactly this Customer", ex2.Message);

        booking.RemoveBookingFromCustomer();
        var ex3 = Assert.Throws<InvalidOperationException>(() => booking.ChangeCustomerInBooking(customer2));
        Assert.AreEqual("It is not possible to assign this booking to a new Customer, because it is not assigned to any", ex3.Message);
    }
}
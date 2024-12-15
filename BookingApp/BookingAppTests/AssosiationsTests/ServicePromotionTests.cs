using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class ServicePromotionTests
{
    [Test]
        public void Test_AddPromotion_Successful()
        {
            var promotion = new Promotion("Holiday Sale", "20% Off", 20);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
            var startDate = new DateTime(2024,12,24,12,00,00);
            var endDate = new DateTime(2024,12,28,12,00,00);

            service.AddPromotion(promotion, startDate, endDate, null);

            Assert.AreEqual(1, service.ServicePromotions.Count);
            Assert.AreEqual(1, promotion.ServicePromotions.Count);
            Assert.AreEqual(promotion, service.ServicePromotions[0].Promotion);
            Assert.AreEqual(service, promotion.ServicePromotions[0].Service);
            Assert.AreEqual(startDate, service.ServicePromotions[0].StartDate);
            Assert.AreEqual(endDate, service.ServicePromotions[0].EndDate);
        }

        [Test]
        public void Test_AddPromotion_ExceptionHandling()
        {
            var promotion = new Promotion("Holiday Sale", "20% Off", 20);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
            var startDate = new DateTime(2024,12,24,12,00,00);
            var endDate = new DateTime(2024,12,28,12,00,00);

            service.AddPromotion(promotion, startDate, endDate, null);

            // Adding a null promotion
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.AddPromotion(null, startDate, endDate, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'promotion')", ex1.Message);

            // Adding an existing promotion
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.AddPromotion(promotion, startDate, endDate, null));
            Assert.AreEqual("This Service is already promoted by this Promotion.", ex2.Message);
        }

        [Test]
        public void Test_RemovePromotion_Successful()
        {
            var promotion = new Promotion("Holiday Sale", "20% Off", 20);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
            var startDate = new DateTime(2024,12,24,12,00,00);
            var endDate = new DateTime(2024,12,28,12,00,00);

            service.AddPromotion(promotion, startDate, endDate, null);
            service.RemovePromotion(promotion);

            Assert.AreEqual(0, service.ServicePromotions.Count);
            Assert.AreEqual(0, promotion.ServicePromotions.Count);
            Assert.IsNull(ServicePromoted.GetAll().FirstOrDefault(sp => sp.Service == service && sp.Promotion == promotion));
        }

        [Test]
        public void Test_RemovePromotion_ExceptionHandling()
        {
            var promotion = new Promotion("Holiday Sale", "20% Off", 20);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

            // Removing from an empty service
            var ex1 = Assert.Throws<InvalidOperationException>(() => service.RemovePromotion(promotion));
            Assert.AreEqual("No matching ServicePromotion found.", ex1.Message);

            // Removing a null promotion
            var ex2 = Assert.Throws<ArgumentNullException>(() => service.RemovePromotion(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'promotion')", ex2.Message);
        }

        [Test]
        public void Test_SubstitutePromotion_Successful()
        {
            var oldPromotion = new Promotion("Holiday Sale", "20% Off", 20);
            var newPromotion = new Promotion("Spring Sale", "25% Off", 25);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
            var startDate = new DateTime(2024,12,24,12,00,00);
            var endDate = new DateTime(2024,12,28,12,00,00);

            service.AddPromotion(oldPromotion, startDate, endDate, null);
            service.SubstitutePromotion(oldPromotion, newPromotion);

            Assert.AreEqual(1, service.ServicePromotions.Count);
            Assert.AreEqual(1, newPromotion.ServicePromotions.Count);
            Assert.AreEqual(newPromotion, service.ServicePromotions[0].Promotion);
            Assert.AreEqual(startDate, service.ServicePromotions[0].StartDate);
            Assert.AreEqual(endDate, service.ServicePromotions[0].EndDate);
            Assert.AreEqual(0, oldPromotion.ServicePromotions.Count);
        }

        [Test]
        public void Test_SubstitutePromotion_ExceptionHandling()
        {
            var oldPromotion = new Promotion("Holiday Sale", "20% Off", 20);
            var newPromotion = new Promotion("Spring Sale", "25% Off", 25);
            var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
            var startDate = new DateTime(2024,12,24,12,00,00);
            var endDate = new DateTime(2024,12,28,12,00,00);

            // Substitute with no existing promotion
            var ex1 = Assert.Throws<Exception>(() => service.SubstitutePromotion(oldPromotion, newPromotion));
            Assert.AreEqual("This old Promotion has not been assigned to this Service", ex1.Message);

            service.AddPromotion(oldPromotion, startDate, endDate, null);

            // Substitute with an already existing promotion
            service.AddPromotion(newPromotion, startDate, endDate, null);
            var ex2 = Assert.Throws<Exception>(() => service.SubstitutePromotion(oldPromotion, newPromotion));
            Assert.AreEqual("This new Promotion is already assigned to this Service", ex2.Message);

            // Substitute with null promotions
            var ex3 = Assert.Throws<ArgumentNullException>(() => service.SubstitutePromotion(null, newPromotion));
            Assert.AreEqual("Value cannot be null. (Parameter 'oldP')", ex3.Message);

            var ex4 = Assert.Throws<ArgumentNullException>(() => service.SubstitutePromotion(oldPromotion, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newP')", ex4.Message);
        }
        
        
}

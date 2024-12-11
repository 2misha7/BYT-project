using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class ServicePromotionTests
{
    [Test]
        public void Test_AddPromotionToService_StartedInService()
        {
            var service = new Service("Haircut", StationCategory.Hair, "Classic haircut service", 25m);
            var promotion = new Promotion("Holiday Discount", "10% off for the holidays", 10);

            service.AddPromotionToService(promotion);

            Assert.AreEqual(1, service.Promotions.Count);
            Assert.AreEqual(1, promotion.Services.Count);
            Assert.AreEqual(promotion, service.Promotions[0]);
            Assert.AreEqual(service, promotion.Services[0]);
        }

        // Test exception handling when adding a promotion to a service (started in Service)
        [Test]
        public void Test_AddPromotionToService_ExceptionHandling()
        {
            var service = new Service("Manicure", StationCategory.Nail, "Deluxe manicure service", 30m);
            var promotion = new Promotion("Summer Sale", "20% off all services", 20);

            // Null promotion
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.AddPromotionToService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'promotion')", ex1.Message);

            // Adding the same promotion twice
            service.AddPromotionToService(promotion);
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.AddPromotionToService(promotion));
            Assert.AreEqual("This Service already has this Promotion.", ex2.Message);
        }

        // Test adding a service to a promotion (started in Promotion)
        [Test]
        public void Test_AddServiceToPromotion_StartedInPromotion()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing full-body massage", 50m);
            var promotion = new Promotion("Weekend Special", "15% off on weekends", 15);

            promotion.AddServiceToPromotion(service);

            Assert.AreEqual(1, service.Promotions.Count);
            Assert.AreEqual(1, promotion.Services.Count);
            Assert.AreEqual(service, promotion.Services[0]);
            Assert.AreEqual(promotion, service.Promotions[0]);
        }

        // Test exception handling when adding a service to a promotion (started in Promotion)
        [Test]
        public void Test_AddServiceToPromotion_ExceptionHandling()
        {
            var service = new Service("Facial", StationCategory.Body, "Rejuvenating facial treatment", 40m);
            var promotion = new Promotion("Winter Sale", "25% off on all skincare", 25);

            // Null service
            var ex1 = Assert.Throws<ArgumentNullException>(() => promotion.AddServiceToPromotion(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

            // Adding the same service twice
            promotion.AddServiceToPromotion(service);
            var ex2 = Assert.Throws<InvalidOperationException>(() => promotion.AddServiceToPromotion(service));
            Assert.AreEqual("This Promotion is already assigned to this Service.", ex2.Message);
        }
        
        [Test]
        public void Test_RemoveServiceFromPromotion_Successful()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing massage", 50m);
            var promotion = new Promotion("Weekend Special", "20% off", 20);

            promotion.AddServiceToPromotion(service);
            promotion.RemoveServiceFromPromotion(service);

            Assert.AreEqual(0, promotion.Services.Count);
            Assert.AreEqual(0, service.Promotions.Count);
        }

        [Test]
        public void Test_RemoveServiceFromPromotion_ExceptionHandling()
        {
            var service = new Service("Facial", StationCategory.Hair, "Rejuvenating facial", 40m);
            var promotion = new Promotion("Holiday Discount", "15% off", 15);

            // Null argument
            var ex1 = Assert.Throws<ArgumentNullException>(() => promotion.RemoveServiceFromPromotion(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

            // Service not in promotion
            var ex2 = Assert.Throws<InvalidOperationException>(() => promotion.RemoveServiceFromPromotion(service));
            Assert.AreEqual("This Promotion is not assigned to this Promotion.", ex2.Message);
        }

        [Test]
        public void Test_SubstituteService_Successful()
        {
            var oldService = new Service("Haircut", StationCategory.Hair, "Classic haircut", 25m);
            var newService = new Service("Manicure", StationCategory.Nail, "Spa manicure", 30m);
            var promotion = new Promotion("Summer Sale", "10% off", 10);

            promotion.AddServiceToPromotion(oldService);
            promotion.SubstituteService(oldService, newService);

            Assert.AreEqual(1, promotion.Services.Count);
            Assert.AreEqual(newService, promotion.Services[0]);
            Assert.AreEqual(0, oldService.Promotions.Count);
            Assert.AreEqual(1, newService.Promotions.Count);
        }

        [Test]
        public void Test_SubstituteService_ExceptionHandling()
        {
            var oldService = new Service("Haircut", StationCategory.Hair, "Classic haircut", 25m);
            var newService = new Service("Manicure", StationCategory.Nail, "Spa manicure", 30m);
            var externalService = new Service("Pedicure", StationCategory.Nail, "Deluxe pedicure", 35m);
            var promotion = new Promotion("Holiday Sale", "20% off", 20);

            promotion.AddServiceToPromotion(oldService);

            // Null arguments
            var ex1 = Assert.Throws<ArgumentNullException>(() => promotion.SubstituteService(null, newService));
            Assert.AreEqual("Value cannot be null. (Parameter 'oldS')", ex1.Message);

            var ex2 = Assert.Throws<ArgumentNullException>(() => promotion.SubstituteService(oldService, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newS')", ex2.Message);

            // Old service not in promotion
            var ex3 = Assert.Throws<Exception>(() => promotion.SubstituteService(externalService, newService));
            Assert.AreEqual("This Promotion does not have this Service", ex3.Message);

            // New service already in promotion
            promotion.AddServiceToPromotion(newService);
            var ex4 = Assert.Throws<Exception>(() => promotion.SubstituteService(oldService, newService));
            Assert.AreEqual("This Promotion already has this Service", ex4.Message);
        }

        [Test]
        public void Test_RemovePromotionFromService_Successful()
        {
            var service = new Service("Pedicure", StationCategory.Nail, "Deluxe pedicure", 35m);
            var promotion = new Promotion("Winter Sale", "25% off", 25);

            service.AddPromotionToService(promotion);
            service.RemovePromotionFromService(promotion);

            Assert.AreEqual(0, service.Promotions.Count);
            Assert.AreEqual(0, promotion.Services.Count);
        }

        [Test]
        public void Test_RemovePromotionFromService_ExceptionHandling()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing massage", 50m);
            var promotion = new Promotion("Spring Sale", "15% off", 15);

            // Null argument
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.RemovePromotionFromService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'promotion')", ex1.Message);

            // Promotion not in service
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.RemovePromotionFromService(promotion));
            Assert.AreEqual("This Service does not have this Promotion.", ex2.Message);
        }

        [Test]
        public void Test_SubstitutePromotion_Successful()
        {
            var oldPromotion = new Promotion("Black Friday", "50% off", 20);
            var newPromotion = new Promotion("Cyber Monday", "40% off", 20);
            var service = new Service("Facial", StationCategory.Hair, "Rejuvenating facial", 40m);

            service.AddPromotionToService(oldPromotion);
            service.SubstitutePromotion(oldPromotion, newPromotion);

            Assert.AreEqual(1, service.Promotions.Count);
            Assert.AreEqual(newPromotion, service.Promotions[0]);
            Assert.AreEqual(0, oldPromotion.Services.Count);
            Assert.AreEqual(1, newPromotion.Services.Count);
        }

        [Test]
        public void Test_SubstitutePromotion_ExceptionHandling()
        {
            var oldPromotion = new Promotion("Black Friday", "50% off", 20);
            var newPromotion = new Promotion("Cyber Monday", "40% off", 20);
            var externalPromotion = new Promotion("Holiday Sale", "25% off", 25);
            var service = new Service("Haircut", StationCategory.Hair, "Classic haircut", 25m);

            service.AddPromotionToService(oldPromotion);

            // Null arguments
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.SubstitutePromotion(null, newPromotion));
            Assert.AreEqual("Value cannot be null. (Parameter 'oldP')", ex1.Message);

            var ex2 = Assert.Throws<ArgumentNullException>(() => service.SubstitutePromotion(oldPromotion, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newP')", ex2.Message);

            // Old promotion not in service
            var ex3 = Assert.Throws<Exception>(() => service.SubstitutePromotion(externalPromotion, newPromotion));
            Assert.AreEqual("This Service does not have this Promotion", ex3.Message);

            // New promotion already in service
            service.AddPromotionToService(newPromotion);
            var ex4 = Assert.Throws<Exception>(() => service.SubstitutePromotion(oldPromotion, newPromotion));
            Assert.AreEqual("This Service already has this Promotion", ex4.Message);
        }
        
}

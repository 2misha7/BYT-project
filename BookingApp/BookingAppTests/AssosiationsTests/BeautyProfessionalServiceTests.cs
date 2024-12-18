using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class BeautyProfessionalServiceTests
{
    [Test]
    public void Test_AddServiceToBeautyProfessional_Successful()
    {
        var specializations = new List<string> { "Hair", "Makeup" };
        var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
            "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
            new RegularAccountType());
        var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

        beautyProfessional.AddServiceToBeautyProfessional(service);

        Assert.AreEqual(1, beautyProfessional.Services.Count);
        Assert.AreEqual(service, beautyProfessional.Services[0]);
        Assert.AreEqual(beautyProfessional, service.BeautyProfessional);
    }
    [Test]
        public void Test_AddServiceToBeautyProfessional_ExceptionHandling()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);
            var specializations2 = new List<string> { "Hair", "Makeup" };
            var beautyProfessional2 = new BeautyProfessional("Jone", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations2,
                new RegularAccountType());

            // Add a null service
            var ex1 = Assert.Throws<ArgumentNullException>(() => beautyProfessional.AddServiceToBeautyProfessional(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

            // Add a service already assigned
            beautyProfessional.AddServiceToBeautyProfessional(service);
            var ex2 = Assert.Throws<InvalidOperationException>(() => beautyProfessional.AddServiceToBeautyProfessional(service));
            Assert.AreEqual("This BeautyPro already has this Service.", ex2.Message);
            
        }
        
        [Test]
        public void Test_AddBeautyProToService_Successful()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            service.AddBeautyProToService(beautyProfessional);

            Assert.AreEqual(beautyProfessional, service.BeautyProfessional);
            Assert.AreEqual(1, beautyProfessional.Services.Count);
            Assert.AreEqual(service, beautyProfessional.Services[0]);
        }

        [Test]
        public void Test_AddBeautyProToService_ExceptionHandling()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);
            var anotherBeautyProfessional = new BeautyProfessional("Jone", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());

            // Add a null BeautyPro
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.AddBeautyProToService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'beautyProfessional')", ex1.Message);

            // Add a BeautyPro when already assigned
            service.AddBeautyProToService(beautyProfessional);
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.AddBeautyProToService(anotherBeautyProfessional));
            Assert.AreEqual("This Service already has a Beauty Professional.", ex2.Message);
        }

        [Test]
        public void Test_RemoveServiceFromBeautyPro_Successful()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            beautyProfessional.AddServiceToBeautyProfessional(service);
            beautyProfessional.RemoveServiceFromBeautyPro(service);

            Assert.AreEqual(0, beautyProfessional.Services.Count);
            Assert.IsNull(service.BeautyProfessional);
        }

        [Test]
        public void Test_RemoveServiceFromBeautyPro_ExceptionHandling()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            // Try to remove a service not assigned
            var ex1 = Assert.Throws<InvalidOperationException>(() => beautyProfessional.RemoveServiceFromBeautyPro(service));
            Assert.AreEqual("This BeautyPro does not have this Service.", ex1.Message);
        }
        [Test]
        public void Test_RemoveBeautyFromService_Successful()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            service.AddBeautyProToService(beautyProfessional);
            service.RemoveBeautyFromService();

            Assert.IsNull(service.BeautyProfessional);
            Assert.AreEqual(0, beautyProfessional.Services.Count);
        }

        [Test]
        public void Test_RemoveBeautyFromService_ExceptionHandling()
        {
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            // Remove a BeautyPro when none is assigned
            var ex1 = Assert.Throws<InvalidOperationException>(() => service.RemoveBeautyFromService());
            Assert.AreEqual("This Service does not have a BeautyPro", ex1.Message);
        }

        [Test]
        public void Test_SubstituteService_Successful()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var oldService = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);
            var newService = new Service("Hair Coloring", StationCategory.Hair, "Professional hair coloring service", 50);

            beautyProfessional.AddServiceToBeautyProfessional(oldService);
            beautyProfessional.SubstituteService(oldService, newService);

            Assert.AreEqual(1, beautyProfessional.Services.Count);
            Assert.AreEqual(newService, beautyProfessional.Services[0]);
            Assert.AreEqual(beautyProfessional, newService.BeautyProfessional);
            Assert.IsNull(oldService.BeautyProfessional);
        }

        [Test]
        public void Test_SubstituteService_ExceptionHandling()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var oldService = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);
            var newService = new Service("Hair Coloring", StationCategory.Hair, "Professional hair coloring service", 50);

            // Substitute with a null service
            var ex1 = Assert.Throws<ArgumentNullException>(() => beautyProfessional.SubstituteService(oldService, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newS')", ex1.Message);

            // Substitute when old service is not assigned
            var ex2 = Assert.Throws<Exception>(() => beautyProfessional.SubstituteService(oldService, newService));
            Assert.AreEqual("This BeautyPro does not have this old Service", ex2.Message);

            // Substitute with a service already assigned
            beautyProfessional.AddServiceToBeautyProfessional(oldService);
            beautyProfessional.AddServiceToBeautyProfessional(newService);
            var ex3 = Assert.Throws<Exception>(() => beautyProfessional.SubstituteService(oldService, newService));
            Assert.AreEqual("This BeautyPro already had this new Service", ex3.Message);
        }

        [Test]
        public void Test_ChangeBeautyPro_Successful()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var beautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            
            var beautyProfessional2 = new BeautyProfessional("Jone", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            beautyProfessional.AddServiceToBeautyProfessional(service);
            service.ChangeBeautyPro(beautyProfessional2);

            Assert.AreEqual(0, beautyProfessional.Services.Count);
            Assert.AreEqual(1, beautyProfessional2.Services.Count);
            Assert.AreEqual(service, beautyProfessional2.Services[0]);
            Assert.AreEqual(beautyProfessional2, service.BeautyProfessional);
        }

        [Test]
        public void Test_ChangeBeautyPro_ExceptionHandling()
        {
            var specializations = new List<string> { "Hair", "Makeup" };
            var oldBeautyProfessional = new BeautyProfessional("Jane", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            
            var newBeautyProfessional = new BeautyProfessional("Jone", "Doe", "jane.doe@example.com", "+1234567890",
                "janedoe", "securePass123", "123 Main St", "Cityville", 100.0m, "5 years", specializations,
                new RegularAccountType());
            var service = new Service("Haircut", StationCategory.Hair, "Professional haircut service", 20);

            // Change BeautyPro to a null one
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.ChangeBeautyPro(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'newBP')", ex1.Message);
            
            // Assign and then change to the same BeautyPro
            oldBeautyProfessional.AddServiceToBeautyProfessional(service);
            var ex3 = Assert.Throws<InvalidOperationException>(() => service.ChangeBeautyPro(oldBeautyProfessional));
            Assert.AreEqual("This Service is already assigned to this BeautyPro", ex3.Message);
        }

}
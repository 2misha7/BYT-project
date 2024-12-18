using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class ServiceBookingTests
{
    [Test]
    public void Test_AssignToBooking_Successful()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        service.AssignToBooking(booking);

        Assert.AreEqual(1, booking.Services.Count);
        Assert.AreEqual(service, booking.Services[0]);
        Assert.AreEqual(booking, service.Booking);
    }

    [Test]
    public void Test_AssignToBooking_ExceptionHandling()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        service.AssignToBooking(booking);

        // Null booking
        var ex1 = Assert.Throws<ArgumentNullException>(() => service.AssignToBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'booking')", ex1.Message);

        // Service already assigned to a booking
        var ex2 = Assert.Throws<InvalidOperationException>(() => service.AssignToBooking(new Booking()));
        Assert.AreEqual("This Service is already in the Booking.", ex2.Message);
    }
    
    [Test]
    public void Test_AddService_Successful()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        booking.AddService(service);

        Assert.AreEqual(1, booking.Services.Count);
        Assert.AreEqual(service, booking.Services[0]);
        Assert.AreEqual(booking, service.Booking);
    }

    [Test]
    public void Test_AddService_ExceptionHandling()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        // Null service
        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.AddService(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

        // Service already added
        booking.AddService(service);
        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.AddService(service));
        Assert.AreEqual("This Booking already has this Service.", ex2.Message);
    }
    [Test]
    public void Test_RemoveFromBooking_Successful()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        service.AssignToBooking(booking);
        service.RemoveFromBooking();

        Assert.AreEqual(0, booking.Services.Count);
        Assert.IsNull(service.Booking);
    }

    [Test]
    public void Test_RemoveFromBooking_ExceptionHandling()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        // Trying to remove service not assigned to any booking
        var ex1 = Assert.Throws<InvalidOperationException>(() => service.RemoveFromBooking());
        Assert.AreEqual("This service is not assigned to a Booking", ex1.Message);
    }
    [Test]
    public void Test_RemoveService_Successful()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        booking.AddService(service);
        booking.RemoveService(service);

        Assert.AreEqual(0, booking.Services.Count);
        Assert.IsNull(service.Booking);
    }
    [Test]
    public void Test_RemoveService_ExceptionHandling()
    {
        var booking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);


        // Trying to remove service that is not assigned to the booking
        var ex1 = Assert.Throws<InvalidOperationException>(() => booking.RemoveService(service));
        Assert.AreEqual("This Booking does not have this Service.", ex1.Message);
        var ex = Assert.Throws<ArgumentNullException>(() => booking.RemoveService(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex.Message);
    }

    [Test]
    public void Test_ChangeBookingAssignedToACoupon_Successful()
    {
        var oldBooking = new Booking();
        var newBooking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        service.AssignToBooking(oldBooking);
        service.ChangeBookingAssignedToACoupon(newBooking);

        Assert.AreEqual(1, newBooking.Services.Count);
        Assert.AreEqual(service, newBooking.Services[0]);
        Assert.AreEqual(newBooking, service.Booking);
    }

    [Test]
    public void Test_ChangeBookingAssignedToACoupon_ExceptionHandling()
    {
        var oldBooking = new Booking();
        var newBooking = new Booking();
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);

        // Change when service is not assigned
        var ex1 = Assert.Throws<InvalidOperationException>(() => service.ChangeBookingAssignedToACoupon(newBooking));
        Assert.AreEqual("It is not possible to assign this Service to a new Booking, because it is not assigned to any", ex1.Message);

        service.AssignToBooking(oldBooking);
        
        // Change to the same booking
        var ex2 = Assert.Throws<InvalidOperationException>(() => service.ChangeBookingAssignedToACoupon(oldBooking));
        Assert.AreEqual("This Booking is already assigned to exactly this Service", ex2.Message);

        // Change to null booking
        var ex3 = Assert.Throws<ArgumentNullException>(() => service.ChangeBookingAssignedToACoupon(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newB')", ex3.Message);
    }
    [Test]
    public void Test_SubstituteService_Successful()
    {
        var booking = new Booking();
        var oldService = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
        var newService = new Service("Brades", StationCategory.Hair, "Basic haircut service", 15);

        booking.AddService(oldService);
        foreach (var VARIABLE in booking.Services)
        {
            Console.WriteLine(VARIABLE.Name);
        }
       
        booking.SubstituteService(oldService, newService);
       
        
        
        Assert.AreEqual(1, booking.Services.Count);
        Assert.AreEqual(newService, booking.Services[0]);
        Assert.AreEqual(booking, newService.Booking);
        Assert.IsNull(oldService.Booking);
    }

    [Test]
    public void Test_SubstituteService_ExceptionHandling()
    {
        var booking = new Booking();
        var oldService =  new Service("Haircut", StationCategory.Hair, "Basic haircut service", 15);
        var newService =  new Service("Brades", StationCategory.Hair, "Basic haircut service", 15);

        // Substitute a service not in the booking
        var ex1 = Assert.Throws<Exception>(() => booking.SubstituteService(oldService, newService));
        Assert.AreEqual("This Booking does not have this old Service", ex1.Message);

        booking.AddService(oldService);
        foreach(var s in booking.Services)
        {
            Console.WriteLine(s.Name);
        }
        // Substitute with a service already in the booking
        booking.AddService(newService);
        foreach(var s in booking.Services)
        {
            Console.WriteLine(s.Name);
        }
        var ex2 = Assert.Throws<Exception>(() => booking.SubstituteService(oldService, newService));
        Assert.AreEqual("This Booking already had this new Service", ex2.Message);
    }
    
   
    [Test]
    public void Test_DeleteService_WillBeDeletedFromBooking()
    {
        var booking = new Booking();
        var service = new Service("name", StationCategory.Body, "desc", 1);
        var service1 = new Service("name", StationCategory.Body, "desc", 1);
        booking.AddService(service);
        booking.AddService(service1);
        
        service.DeleteService();
        Assert.AreEqual(1, booking.Services.Count);
        Assert.AreEqual(service1, booking.Services[0]);
        
        
    }
    
    
    [Test]
    public void Test_DeleteBooking_ServiceRemainsInTheSystem()
    {
        var booking = new Booking();
        var service = new Service("name", StationCategory.Body, "desc", 1);
        var service1 = new Service("name", StationCategory.Body, "desc", 1);
        booking.AddService(service);
        booking.AddService(service1);
       
        
        booking.DeleteBooking();
        Assert.IsNull(service.Booking);
        Assert.IsNull(service1.Booking);
    }
}
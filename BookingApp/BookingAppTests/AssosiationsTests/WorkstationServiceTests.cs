using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class WorkstationServiceTests
{
    [Test]
    public void Test_AddServiceAtTime_Successful()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = new DateTime(2024,12,30, 00,00,00);

        workStation.AddServiceAtTime(service, dateTime);

        Assert.AreEqual(1, workStation.ServicesByTime.Count);
        Assert.AreEqual(service, workStation.ServicesByTime[dateTime]);
        Assert.AreEqual(workStation, service.AssignedWorkStation);
    }
    [Test]
    public void Test_AddServiceAtTime_ExceptionHandling()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service1 = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var service2 = new Service("Hair Wash", StationCategory.Hair, "Hair washing service", 10);
        var mismatchedService = new Service("Manicure", StationCategory.Nail, "Manicure service", 30);
        var dateTime = DateTime.Now;

        workStation.AddServiceAtTime(service1, dateTime);

        // Add a null service
        var ex1 = Assert.Throws<ArgumentNullException>(() => workStation.AddServiceAtTime(null, dateTime));
        Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

        // Add a service to a time slot already occupied
        var ex2 = Assert.Throws<InvalidOperationException>(() => workStation.AddServiceAtTime(service2, dateTime));
        Assert.AreEqual($"A Service is already scheduled at {dateTime} for this WorkStation.", ex2.Message);

        // Add a service with a mismatched category
        var ex3 = Assert.Throws<InvalidOperationException>(() => workStation.AddServiceAtTime(mismatchedService, DateTime.Now.AddHours(1)));
        Assert.AreEqual("Category of workstation and service must be the same", ex3.Message);
    }
    
    [Test]
    public void Test_AssignWorkStationAndTime_Successful()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = DateTime.Now;

        service.AssignWorkStationAndTime(workStation, dateTime);

        Assert.AreEqual(1, workStation.ServicesByTime.Count);
        Assert.AreEqual(service, workStation.ServicesByTime[dateTime]);
        Assert.AreEqual(workStation, service.AssignedWorkStation);
    }
    [Test]
    public void Test_AssignWorkStationAndTime_ExceptionHandling()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var mismatchedService = new Service("Manicure", StationCategory.Nail, "Manicure service", 30);
        var dateTime = DateTime.Now;

        // Assign a null workstation
        var ex1 = Assert.Throws<ArgumentNullException>(() => service.AssignWorkStationAndTime(null, dateTime));
        Assert.AreEqual("Value cannot be null. (Parameter 'workStation')", ex1.Message);

        // Assign mismatched categories
        var ex2 = Assert.Throws<InvalidOperationException>(() => mismatchedService.AssignWorkStationAndTime(workStation, dateTime));
        Assert.AreEqual("Category of workstation and service must be the same", ex2.Message);

        // Assign a workstation already assigned
        service.AssignWorkStationAndTime(workStation, dateTime);
        var ex3 = Assert.Throws<InvalidOperationException>(() => service.AssignWorkStationAndTime(workStation, DateTime.Now.AddHours(1)));
        Assert.AreEqual("This Service is already assigned to a WorkStation.", ex3.Message);
    }
    
    [Test]
    public void Test_RemoveServiceAtTime_Successful()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = DateTime.Now;

        workStation.AddServiceAtTime(service, dateTime);
        workStation.RemoveServiceAtTime(service);

        Assert.AreEqual(0, workStation.ServicesByTime.Count);
        Assert.IsNull(service.AssignedWorkStation);
    }
    [Test]
    public void Test_RemoveServiceAtTime_ExceptionHandling()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);

        // Try removing a service not assigned
        var ex1 = Assert.Throws<InvalidOperationException>(() => workStation.RemoveServiceAtTime(service));
        Assert.AreEqual("No Service is scheduled for this WorkStation.", ex1.Message);
    }
    [Test]
    public void Test_RemoveWorkStationAndTime_Successful()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = DateTime.Now;

        service.AssignWorkStationAndTime(workStation, dateTime);
        service.RemoveWorkStationAndTime();

        Assert.AreEqual(0, workStation.ServicesByTime.Count);
        Assert.IsNull(service.AssignedWorkStation);
    }
    [Test]
    public void Test_RemoveWorkStationAndTime_ExceptionHandling()
    {
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);

        // Remove workstation when none is assigned
        var ex = Assert.Throws<InvalidOperationException>(() => service.RemoveWorkStationAndTime());
        Assert.AreEqual("This Service is not assigned to a WorkStation.", ex.Message);
    }
    [Test]
    public void Test_ChangeServiceAtTime_Successful()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var oldService = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var newService = new Service("Hair Wash", StationCategory.Hair, "Hair washing service", 10);
        var dateTime = DateTime.Now;

        workStation.AddServiceAtTime(oldService, dateTime);
        workStation.ChangeServiceAtTime(dateTime, newService);

        Assert.AreEqual(1, workStation.ServicesByTime.Count);
        Assert.AreEqual(newService, workStation.ServicesByTime[dateTime]);
        Assert.AreEqual(workStation, newService.AssignedWorkStation);
        Assert.IsNull(oldService.AssignedWorkStation);
    }
    [Test]
    public void Test_ChangeServiceAtTime_ExceptionHandling()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var newService = new Service("Hair Wash", StationCategory.Hair, "Hair washing service", 10);
        var dateTime = DateTime.Now;

        // Change service at a non-existent time slot
        var ex1 = Assert.Throws<InvalidOperationException>(() => workStation.ChangeServiceAtTime(dateTime, newService));
        Assert.AreEqual($"No Service is scheduled at {dateTime} for this WorkStation.", ex1.Message);

        // Change service with a null service
        workStation.AddServiceAtTime(service, dateTime);
        var ex2 = Assert.Throws<ArgumentNullException>(() => workStation.ChangeServiceAtTime(dateTime, null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newService')", ex2.Message);
    }
    [Test]
    public void Test_ChangeWorkStation_Successful()
    {
        var workStation1 = new WorkStation(StationCategory.Hair, 100);
        var workStation2 = new WorkStation(StationCategory.Hair, 150);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = DateTime.Now;

        service.AssignWorkStationAndTime(workStation1, dateTime);
        service.ChangeWorkStation(workStation2);

        Assert.AreEqual(0, workStation1.ServicesByTime.Count);
        Assert.AreEqual(1, workStation2.ServicesByTime.Count);
        Assert.AreEqual(service, workStation2.ServicesByTime[dateTime]);
        Assert.AreEqual(workStation2, service.AssignedWorkStation);
    }
    [Test]
    public void Test_ChangeWorkStation_ExceptionHandling()
    {
        var workStation1 = new WorkStation(StationCategory.Hair, 100);
        var workStation2 = new WorkStation(StationCategory.Hair, 150);
        var mismatchedWorkStation = new WorkStation(StationCategory.Nail, 200);
        var service = new Service("Haircut", StationCategory.Hair, "Basic haircut service", 20);
        var dateTime = DateTime.Now;

        // Change workstation when none is assigned
        var ex1 = Assert.Throws<InvalidOperationException>(() => service.ChangeWorkStation(workStation1));
        Assert.AreEqual("It is impossible to assign new Workstation to this Service, as there is no Workstation assigned before", ex1.Message);

        service.AssignWorkStationAndTime(workStation1, dateTime);

        // Change to the same workstation
        var ex2 = Assert.Throws<InvalidOperationException>(() => service.ChangeWorkStation(workStation1));
        Assert.AreEqual("This Service is already assigned to the specified WorkStation.", ex2.Message);

        // Change to a mismatched category workstation
        var ex3 = Assert.Throws<InvalidOperationException>(() => service.ChangeWorkStation(mismatchedWorkStation));
        Assert.AreEqual("The WorkStation's category does not match the Service's category.", ex3.Message);
    }
}
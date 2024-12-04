using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class CoworkingWorkstationTests
{
    //add workstation to coworking,  creation of object references started in Workstation Class
    [Test]
    public void Test_AddWorkStationToCoworking_StartedInWorkstation()
    {
        
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        workStation.AddWorkstationToCoworking(coworkingSpace);
        Assert.AreEqual(1, coworkingSpace.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, coworkingSpace);
        Assert.AreEqual(workStation, coworkingSpace.WorkStations[0]);
    }
    
    [Test]
    public void Test_AddWorkStationToCoworking_StartedInWorkstation_ExceptionHandling()
    {
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => workStation.AddWorkstationToCoworking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'coworkingSpace')", ex1.Message);
        
        workStation.AddWorkstationToCoworking(coworkingSpace);
        
        var ex2 = Assert.Throws<InvalidOperationException>(() => workStation.AddWorkstationToCoworking(coworkingSpace));
        Assert.AreEqual("This WorkStation is already assigned to a Coworking Space. Remove it first before reassigning.", ex2.Message);
    }
    
    
    //add workstation to coworking,  creation of object references started in CoworkingSpace Class
    [Test]
    public void Test_AddWorkStationToCoworking_StartedInCoworkingSpace()
    {
        
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        coworkingSpace.AddWorkStation(workStation);
        
        Assert.AreEqual(1, coworkingSpace.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, coworkingSpace);
        Assert.AreEqual(workStation, coworkingSpace.WorkStations[0]);
    }
    [Test]
    public void Test_AddWorkStationToCoworking_StartedInCoworkingSpace_ExceptionHandling()
    {
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => coworkingSpace.AddWorkStation(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'workStation')", ex1.Message);
        
        coworkingSpace.AddWorkStation(workStation);
        
        var ex2 = Assert.Throws<InvalidOperationException>(() => coworkingSpace.AddWorkStation(workStation));
        Assert.AreEqual("This WorkStation is already part of this Coworking Space.", ex2.Message);
    }
    
    
    //remove workstation from coworking,  removing of object references started in Workstation Class
    [Test]
    public void Test_RemoveWorkStationFromCoworking_StartedInWorkstation()
    {
        
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        workStation.AddWorkstationToCoworking(coworkingSpace);
        
        workStation.RemoveWorkstationFromCoworkingSpace();
       
        Assert.AreEqual(0, coworkingSpace.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, null);
    }
    
    [Test]
    public void Test_RemoveWorkStationFromCoworking_StartedInWorkstation_ExceptionHandling()
    {
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        var ex = Assert.Throws<InvalidOperationException>(() => workStation.RemoveWorkstationFromCoworkingSpace());
        Assert.AreEqual("This workstation is not assigned to a Coworking Space", ex.Message);
    }
    
    //remove workstation from coworking,  removing of object references started in Coworking Space Class
    [Test]
    public void Test_RemoveWorkStationFromCoworking_StartedInCoworkingSpace()
    {
        
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        coworkingSpace.AddWorkStation(workStation);
        
        coworkingSpace.RemoveWorkStationFromCoworking(workStation);
       
        Assert.AreEqual(0, coworkingSpace.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, null);
        
    }
    [Test]
    public void Test_RemoveWorkStationFromCoworking_StartedInCoworkingSpace_ExceptionHandling()
    {
        var coworkingSpace = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => coworkingSpace.RemoveWorkStationFromCoworking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'workStation')", ex1.Message);
        
        coworkingSpace.AddWorkStation(workStation);
        
        var ex2 = Assert.Throws<InvalidOperationException>(() => coworkingSpace.RemoveWorkStationFromCoworking(new WorkStation(StationCategory.Nail, 150)));
        Assert.AreEqual("This WorkStation is not part of this CoworkingSpace.", ex2.Message);

    }

    
    //move workstation to another coworking,  started in Workstation Class
    [Test]
    public void Test_ChangeCoworkingWorkstationIn_StartedInWorkstation()
    {
        
        var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var coworkingSpace2 = new CoworkingSpace("123 South St", "Warsaw", "+1234757890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        workStation.AddWorkstationToCoworking(coworkingSpace1);
        
        workStation.ChangeCoworkingWorkstationIn(coworkingSpace2);
       
        Assert.AreEqual(0, coworkingSpace1.WorkStations.Count);
        Assert.AreEqual(1, coworkingSpace2.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, coworkingSpace2);
        Assert.AreEqual(workStation, coworkingSpace2.WorkStations[0]);
        
    }
    [Test]
    public void Test_ChangeCoworkingWorkstationIn_StartedInWorkstation_ExceptionHandling()
    {
        
        var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var coworkingSpace2 = new CoworkingSpace("456 Elm St", "Los Angeles", "+1987654321");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => workStation.ChangeCoworkingWorkstationIn(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newCoworkingSpace')", ex1.Message);
        
        workStation.AddWorkstationToCoworking(coworkingSpace1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => workStation.ChangeCoworkingWorkstationIn(coworkingSpace1));
        Assert.AreEqual("This Workstation is already assigned to exactly this Coworking Space", ex2.Message);
        
        var workStation2 = new WorkStation(StationCategory.Nail, 150);
        var ex3 = Assert.Throws<InvalidOperationException>(() => workStation2.ChangeCoworkingWorkstationIn(coworkingSpace2));
        Assert.AreEqual("It is not possible to place worsktation in another coworking, because it is not assigned to any", ex3.Message);
        
    }
    
    //move workstation to another coworking,  started in CoworkingSpace Class
    [Test]
    public void Test_ChangeCoworkingWorkstationIn_StartedInCoworkingSpace()
    {
        
        var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var coworkingSpace2 = new CoworkingSpace("123 South St", "Warsaw", "+1234757890");
        var workStation = new WorkStation(StationCategory.Hair, 100);
        coworkingSpace1.AddWorkStation(workStation);
        
        coworkingSpace2.AddWorkstationFromAnotherCoworking(workStation);
        
        Assert.AreEqual(0, coworkingSpace1.WorkStations.Count);
        Assert.AreEqual(1, coworkingSpace2.WorkStations.Count);
        Assert.AreEqual(workStation.CoworkingSpace, coworkingSpace2);
        Assert.AreEqual(workStation, coworkingSpace2.WorkStations[0]);
        
    }
    
    [Test]
        public void Test_ChangeCoworkingWorkstationIn_StartedInCoworkingSpace_ExceptionHandling()
        {
            
            var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
            
            var workStation = new WorkStation(StationCategory.Hair, 100);

            
            var ex1 = Assert.Throws<ArgumentNullException>(() => coworkingSpace1.AddWorkstationFromAnotherCoworking(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'workStation')", ex1.Message);

           
           coworkingSpace1.AddWorkStation(workStation);

           
           var ex2 = Assert.Throws<Exception>(() => coworkingSpace1.AddWorkstationFromAnotherCoworking(workStation));
           Assert.AreEqual("This Workstation is already in this Coworking space", ex2.Message);

         
           var unassignedWorkStation = new WorkStation(StationCategory.Nail, 150);
           var ex3 = Assert.Throws<Exception>(() => coworkingSpace1.AddWorkstationFromAnotherCoworking(unassignedWorkStation));
           Assert.AreEqual("It is not possible to add this Workstation from another CoworkingSpace, as it is not assigned to any", ex3.Message);
           
        
    }
    
    //Delete workstation
    [Test]
    public void Test_DeleteWorkstation_WillBeDeletedFromCoworkingSpace()
    {
        var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation1 = new WorkStation(StationCategory.Hair, 100);
        var workStation2 = new WorkStation(StationCategory.Nail, 150);
        coworkingSpace1.AddWorkStation(workStation1);
        coworkingSpace1.AddWorkStation(workStation2);
        Assert.AreEqual(2, coworkingSpace1.WorkStations.Count);
        workStation1.DeleteWorkstation();
        Assert.AreEqual(1, coworkingSpace1.WorkStations.Count);
        Assert.AreEqual(workStation2, coworkingSpace1.WorkStations[0]);
    }
    
    //Delete CoworkingSpace
    [Test]
    public void Test_DeleteCoworkingSpace_WorkstationsRemainButCoworkingsNotAssigned()
    {
        var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
        var workStation1 = new WorkStation(StationCategory.Hair, 100);
        var workStation2 = new WorkStation(StationCategory.Nail, 150);
        coworkingSpace1.AddWorkStation(workStation1);
        coworkingSpace1.AddWorkStation(workStation2);
       
        
        coworkingSpace1.DeleteCoworkingSpace();
        Assert.IsNull(workStation1.CoworkingSpace);
        Assert.IsNull(workStation2.CoworkingSpace);
    }
}
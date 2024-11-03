using BookingApp.Models;
namespace BookingAppTests;

//tests for all getter and setter and exception
// tests for all class extents is everything saved and stored correctly

public class AttributeCorrectnessTests
{
    //Check if every attribute in your diagram was implemented correctly :
    // ○ One test to check if gets the correct information back
    // ○ A few tests (depending on the amount of exceptions) to showcase if the the exceptions are being
    //     thrown correctly 
 
    
    //WorkStation 
    //one test to check if we get correct info back
    [Test]
    public void WorkstationCreate_GetCorrectInfoBack()
    {
        var category = StationCategory.Hair;
        decimal price = 50;
        
        var workStation = new WorkStation(category, price);
        
        Assert.That(workStation.Category, Is.EqualTo(category));
        Assert.That(workStation.Price, Is.EqualTo(price));
    }

    //check all exceptions, not possible to create an object without passing validation
    [Test]
    public void Workstation_ThrowsExceptionWithNegativePrice()
    {
        StationCategory category = StationCategory.Makeup;
        decimal negativePrice = (decimal)-10.0;
        
        var ex = Assert.Throws<ArgumentException>(() => new WorkStation(category, negativePrice));
        Assert.AreEqual("Price cannot be negative", ex.Message); 
    }
    //check id initialization 
    [Test]
    public void WorkStation_IdIsAssignedCorrectly()
    {
        var category = StationCategory.Body;
        var price = 100.00m;
        
        var firstWorkStation = new WorkStation(category, price);
        var secondWorkStation = new WorkStation(category, 150.00m);
        
        Assert.AreEqual(1, firstWorkStation.Id);
        Assert.AreEqual(2, secondWorkStation.Id);
    }
    
}
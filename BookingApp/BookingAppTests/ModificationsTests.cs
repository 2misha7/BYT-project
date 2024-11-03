using BookingApp.Models;

namespace BookingAppTests;

//A unit test for checking if the principle of encapsulation is met (if I modify an attribute
//without using a constructor would it change the values in the class extent)
public class ModificationsTests
{
    //Workstation 
    [Test]
    public void Workstation_ModifyToIncorrectPrice_ThrowsException()
    {
        var category = StationCategory.Hair;
        decimal price = 50;
        
        var workStation = new WorkStation(category, price);
        
        var ex = Assert.Throws<ArgumentException>(() => workStation.Price = -10);
        Assert.AreEqual("Price cannot be negative", ex.Message);
        
    }
    
}
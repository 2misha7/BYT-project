using BookingApp;
using BookingApp.Models;

namespace BookingAppTests;

//A unit test for checking if the principle of encapsulation is met (if I modify an attribute
//without using a constructor would it change the values in the class extent)
public class ModificationsTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
        var workstation = new WorkStation(StationCategory.Body, 10m);
        var serviceBooked = new ServiceBooked(DateTime.Now);
        var service = new Service("Name", StationCategory.Body, "Description", 10m);
        var review = new Review(ReviewRating.Awful, "Comment", DateTime.Now);
        var promotion = new Promotion("Name", "Description", 10);
        
        Repository.WriteAllToFile();
        Repository.GetAllFromFile();

    }
    //Workstation 
    [Test]
    public void Workstation_ObjectFromFileModified_ClassExtentNotModified()
    {
        var workstation = WorkStation.GetAll().FirstOrDefault();
        workstation.Category = StationCategory.Hair;


        var sameWorkstationInExtent = WorkStation.GetAll().FirstOrDefault(w => w.Id == workstation.Id);
        Assert.AreEqual(workstation.Category, StationCategory.Hair);
        Assert.AreEqual(sameWorkstationInExtent.Category, StationCategory.Body);
    }
    [Test]
    public void Workstation_NewObjectModified_ClassExtentNotModified()
    {
        var newWorkstation = new WorkStation(StationCategory.Makeup, 15m);

        newWorkstation.Category = StationCategory.Skin;
        var sameWorkstationInExtent = WorkStation.GetAll().FirstOrDefault(w => w.Id == newWorkstation.Id);
        
        
        Assert.AreEqual(newWorkstation.Category, StationCategory.Skin);
        Assert.AreEqual(sameWorkstationInExtent.Category, StationCategory.Makeup);
    }

    
    //ServiceBooked
    [Test]
    public void ServiceBooked_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Service 
    [Test]
    public void Service_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Review 
    [Test]
    public void Review_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Promotion
    [Test]
    public void Promotion_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Post 
    [Test]
    public void Post_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //PortfolioPage 
    [Test]
    public void PortfolioPage_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Payment 
    [Test]
    public void Payment_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Notification 
    [Test]
    public void Notification_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Customer 
    [Test]
    public void Customer_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //CoworkingSpace 
    [Test]
    public void CoworkingSpace_ObjectModified_ClassExtentNotModified()
    {
        
    }
    //Coupon 
    [Test]
    public void Coupon_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //Booking 
    [Test]
    public void Booking_ObjectModified_ClassExtentNotModified()
    {
        
    }
    
    //BeautyProfessional 
    [Test]
    public void BeautyProfessional_ObjectModified_ClassExtentNotModified()
    {
        
    }
}
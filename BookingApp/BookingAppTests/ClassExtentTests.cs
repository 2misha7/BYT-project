using BookingApp;
using BookingApp.Models;

namespace BookingAppTests;

public class ClassExtentTests
{
    //A unit test for checking if your class extent is storing the correct classes
    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
        Repository.GetAllFromFile();
    }
    //Workstation
    [Test]
    public void WorkStationExtent_StoresCorrectClass()
    {
        //return a reference
        var workStation = new WorkStation(StationCategory.Body, 100m);
        Assert.AreEqual(1, WorkStation.GetAll().Count);
        Assert.IsTrue(WorkStation.GetAll().Contains(workStation));
    }
    
    //ServiceBooked
    [Test]
    public void ServiceBookedExtent_StoresCorrectClass()
    {
        var serviceBooked = new ServiceBooked(DateTime.Now.AddDays(1));
        Assert.AreEqual(1, ServiceBooked.GetAll().Count);
        Assert.IsTrue(ServiceBooked.GetAll().Contains(serviceBooked));
    }
    
    //Service
    [Test]
    public void ServiceExtent_StoresCorrectClass()
    {
        var service = new Service("NewService", StationCategory.Body, "Description", 10m);
        Assert.AreEqual(1, Service.GetAll().Count);
        Assert.IsTrue(Service.GetAll().Contains(service));
    }
    
    //Review
    [Test]
    public void ReviewExtent_StoresCorrectClass()
    {
        var review = new Review(ReviewRating.Awful, "comment", DateTime.Now);
        Assert.AreEqual(1, Review.GetAll().Count);
        Assert.IsTrue(Review.GetAll().Contains(review));
    }
    
    //Promotion
    [Test]
    public void PromotionExtent_StoresCorrectClass()
    {
        var promotion = new Promotion("name", "description", 10);
        Assert.AreEqual(1, Promotion.GetAll().Count);
        Assert.IsTrue(Promotion.GetAll().Contains(promotion));
    }
    //Post
    [Test]
    public void PostExtent_StoresCorrectClass()
    {
        var post = new Post("link", "text");
        Assert.AreEqual(1, Post.GetAll().Count);
        Assert.IsTrue(Post.GetAll().Contains(post));
    }
    
    //PortfolioPage
    [Test]
    public void PortfolioPageExtent_StoresCorrectClass()
    {
        var portfolioPage = new PortfolioPage("description");
        Assert.AreEqual(1, PortfolioPage.GetAll().Count);
        Assert.IsTrue(PortfolioPage.GetAll().Contains(portfolioPage));
    }
    
    //Payment
    [Test]
    public void PaymentExtent_StoresCorrectClass()
    {
        var payment = new Payment(10m, "CouponCode");
        Assert.AreEqual(1, Payment.GetAll().Count);
        Assert.IsTrue(Payment.GetAll().Contains(payment));
    }
    
    //Notification
    [Test]
    public void NotificationExtent_StoresCorrectClass()
    {
        var notification = new Notification("Notification");
        Assert.AreEqual(1, Notification.GetAll().Count);
        Assert.IsTrue(Notification.GetAll().Contains(notification));
    }
    
    //Customer
    [Test]
    public void CustomerExtent_StoresCorrectClass()
    {
        var customer = new Customer("name", "surname", "email@gmail.com", "789456123", "login", "ASDFpoi!234",
            "address", "Warsaw", 10m, new RegularAccountType());
        Assert.AreEqual(1, Customer.GetAll().Count);
        Assert.IsTrue(Customer.GetAll().Contains(customer));
    }
    
    //CoworkingSpace
    [Test]
    public void CoworkingSpaceExtent_StoresCorrectClass()
    {
        var coworkingSpace = new CoworkingSpace("address", "warsaw", "789456123");
        Assert.AreEqual(1, CoworkingSpace.GetAll().Count);
        Assert.IsTrue(CoworkingSpace.GetAll().Contains(coworkingSpace));
    }
    
    //Coupon
    [Test]
    public void CouponExtent_StoresCorrectClass()
    {
        DateTime validFrom = DateTime.Now;
        DateTime validTo = validFrom.AddDays(30);
        var coupon = new Coupon("code", "description", 10, validFrom, validTo);
        Assert.AreEqual(1, Coupon.GetAll().Count);
        Assert.IsTrue(Coupon.GetAll().Contains(coupon));
    }
    
    //Booking
    [Test]
    public void BookingExtent_StoresCorrectClass()
    {
        var booking = new Booking();
        Assert.AreEqual(1, Booking.GetAll().Count);
        Assert.IsTrue(Booking.GetAll().Contains(booking));
    }
    
    //BeautyProfessional
    [Test]
    public void BeautyProfessionalExtent_StoresCorrectClass()
    {
        var specializations = new[] { "Hair" };
        var beautyProf = new BeautyProfessional("name", "surname", "email@gmail.com", "789456123", "login", "ASDFpoi!234",
            "address", "Warsaw", 10m, "experienced", specializations, new RegularAccountType());
        Assert.AreEqual(1, BeautyProfessional.GetAll().Count);
        Assert.IsTrue(BeautyProfessional.GetAll().Contains(beautyProf));
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
    }
    
}
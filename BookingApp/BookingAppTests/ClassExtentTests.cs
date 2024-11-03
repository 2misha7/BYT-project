using BookingApp.Models;

namespace BookingAppTests;

public class ClassExtentTests
{
    //misha
    //A unit test for checking if your class extent is storing the correct classes
    
    //Workstation
    [Test]
    public void WorkStationExtent_StoresCorrectClass()
    {
        var workStation = new WorkStation(StationCategory.Body, 100m);
        Assert.AreEqual(1, WorkStation.All().Count);
        Assert.IsTrue(WorkStation.All().Contains(workStation));
    }
    
    //ServiceBooked
    [Test]
    public void ServiceBookedExtent_StoresCorrectClass()
    {
        var serviceBooked = new ServiceBooked(DateTime.Now.AddDays(1));
        Assert.AreEqual(1, ServiceBooked.All().Count);
        Assert.IsTrue(ServiceBooked.All().Contains(serviceBooked));
    }
    
    //Service
    [Test]
    public void ServiceExtent_StoresCorrectClass()
    {
        var service = new Service("NewService", StationCategory.Body, "Description", 10m);
        Assert.AreEqual(1, Service.All().Count);
        Assert.IsTrue(Service.All().Contains(service));
    }
    
    //Review
    [Test]
    public void ReviewExtent_StoresCorrectClass()
    {
        var review = new Review(ReviewRating.Awful, "comment", DateTime.Now);
        Assert.AreEqual(1, Review.All().Count);
        Assert.IsTrue(Review.All().Contains(review));
    }
    
    //Promotion
    [Test]
    public void PromotionExtent_StoresCorrectClass()
    {
        var promotion = new Promotion("name", "description", 10);
        Assert.AreEqual(1, Promotion.All().Count);
        Assert.IsTrue(Promotion.All().Contains(promotion));
    }
    //Post
    [Test]
    public void PostExtent_StoresCorrectClass()
    {
        var post = new Post("link", "text");
        Assert.AreEqual(1, Post.All().Count);
        Assert.IsTrue(Post.All().Contains(post));
    }
    
    //PortfolioPage
    [Test]
    public void PortfolioPageExtent_StoresCorrectClass()
    {
        var portfolioPage = new PortfolioPage("description");
        Assert.AreEqual(1, PortfolioPage.All().Count);
        Assert.IsTrue(PortfolioPage.All().Contains(portfolioPage));
    }
    
    //Payment
    [Test]
    public void PaymentExtent_StoresCorrectClass()
    {
        var payment = new Payment(10m, "CouponCode");
        Assert.AreEqual(1, Payment.All().Count);
        Assert.IsTrue(Payment.All().Contains(payment));
    }
    
    //Notification
    [Test]
    public void NotificationExtent_StoresCorrectClass()
    {
        var notification = new Notification("Notification");
        Assert.AreEqual(1, Notification.All().Count);
        Assert.IsTrue(Notification.All().Contains(notification));
    }
    
    //Customer
    [Test]
    public void CustomerExtent_StoresCorrectClass()
    {
        var customer = new Customer("name", "surname", "email@gmail.com", "789456123", "login", "ASDFpoi!234",
            "address", "Warsaw", 10m, new RegularAccountType());
        Assert.AreEqual(1, Customer.All().Count);
        Assert.IsTrue(Customer.All().Contains(customer));
    }
    
    //CoworkingSpace
    [Test]
    public void CoworkingSpaceExtent_StoresCorrectClass()
    {
        var coworkingSpace = new CoworkingSpace("address", "warsaw", "789456123");
        Assert.AreEqual(1, CoworkingSpace.All().Count);
        Assert.IsTrue(CoworkingSpace.All().Contains(coworkingSpace));
    }
    
    //Coupon
    [Test]
    public void CouponExtent_StoresCorrectClass()
    {
        DateTime validFrom = DateTime.Now;
        DateTime validTo = validFrom.AddDays(30);
        var coupon = new Coupon("code", "description", 10, validFrom, validTo);
        Assert.AreEqual(1, Coupon.All().Count);
        Assert.IsTrue(Coupon.All().Contains(coupon));
    }
    
    //Booking
    [Test]
    public void BookingExtent_StoresCorrectClass()
    {
        var booking = new Booking();
        Assert.AreEqual(1, Booking.All().Count);
        Assert.IsTrue(Booking.All().Contains(booking));
    }
    
    //BeautyProfessional
    [Test]
    public void BeautyProfessionalExtent_StoresCorrectClass()
    {
        var specializations = new[] { "Hair" };
        var beautyProf = new BeautyProfessional("name", "surname", "email@gmail.com", "789456123", "login", "ASDFpoi!234",
            "address", "Warsaw", 10m, "experienced", specializations, new RegularAccountType());
        Assert.AreEqual(1, BeautyProfessional.All().Count);
        Assert.IsTrue(BeautyProfessional.All().Contains(beautyProf));
    }
    
    
    
}
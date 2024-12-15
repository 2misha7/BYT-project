using BookingApp;
using BookingApp.Models;

namespace BookingAppTests;

//A unit test that checks for extent presestiency (are the files stored correctly and are they
//retrieved correctly after each new run) 
public class ExtentPersistencyTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
        Repository.GetAllFromFile();
        var workstation = new WorkStation(StationCategory.Body, 10m);
        //var serviceBooked = new ServicePromoted(new DateTime(2025, 1, 1, 14, 30, 0));
        var service = new Service("Name", StationCategory.Body, "Description", 10m);
        var review = new Review(ReviewRating.Awful, "Comment", DateTime.Now);
        var promotion = new Promotion("Name", "Description", 10);
        var post = new Post("link", "text");
        var portfolioPage = new PortfolioPage("description");
        var payment = new Payment(10m, "C1");
        var notification = new Notification("text");
        var customer = new Customer("John", "Doe", "john.doe@example.com", "+123456789", "johndoe", "password123",
            "street", "city", 100m, new RegularAccountType());
        var coworkingSpace = new CoworkingSpace("street", "Warsaw", "+123456789");
        var coupon = new Coupon("DISCOUNT10", "text", 10, DateTime.Now,
            DateTime.Now.AddMonths(1));
        var beautyProfessional = new BeautyProfessional("anna","smith","anna@gmail.com","+489595995","smith","123",
            "street","Warsaw",1m,"1 year",new List<string> { "makeup" }, new RegularAccountType());
        var booking = new Booking();
        Repository.WriteAllToFile();
    }

    [Test]
    public void WorkStation_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();
        var firstLoadWorkStations = WorkStation.GetAll();
        
        Assert.AreEqual(1, firstLoadWorkStations.Count, "WorkStation count mismatch.");
        Assert.AreEqual(StationCategory.Body, firstLoadWorkStations.First().Category, "WorkStation Category mismatch.");
        Assert.AreEqual(10m, firstLoadWorkStations.First().Price, "WorkStation Price mismatch.");
    }
    
    
    
    [Test]
    public void Service_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadService = Service.GetAll();

        Assert.AreEqual(1, firstLoadService.Count);
        Assert.AreEqual("Name", firstLoadService.First().Name);
        Assert.AreEqual(StationCategory.Body, firstLoadService.First().ServiceCategory);
        Assert.AreEqual("Description", firstLoadService.First().Description);
        Assert.AreEqual(10m, firstLoadService.First().Price);
    }

    [Test]
    public void Review_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadReview = Review.GetAll();

        Assert.AreEqual(1, firstLoadReview.Count);
        Assert.AreEqual(ReviewRating.Awful, firstLoadReview.First().Rating);
        Assert.AreEqual("Comment", firstLoadReview.First().Comment);
    }

    [Test]
    public void Promotion_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadPromotion = Promotion.GetAll();

        Assert.AreEqual(1, firstLoadPromotion.Count);
        Assert.AreEqual("Name", firstLoadPromotion.First().Name);
        Assert.AreEqual("Description", firstLoadPromotion.First().DiscountDescription);
        Assert.AreEqual(10, firstLoadPromotion.First().TotalDiscountPercentage);
    }
    [Test]
    public void Post_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadPost = Post.GetAll();

        Assert.AreEqual(1, firstLoadPost.Count);
        Assert.AreEqual("link", firstLoadPost.First().ImageLink);
        Assert.AreEqual("text", firstLoadPost.First().Text);
    }

    [Test]
    public void PortfolioPage_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadPortfolioPage = PortfolioPage.GetAll();

        Assert.AreEqual(1, firstLoadPortfolioPage.Count);
        Assert.AreEqual("description", firstLoadPortfolioPage.First().Description);
    }

    [Test]
    public void Payment_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadPayment = Payment.GetAll();

        Assert.AreEqual(1, firstLoadPayment.Count);
        Assert.AreEqual(10m, firstLoadPayment.First().FinalAmount);
        Assert.AreEqual("C1", firstLoadPayment.First().CouponCode);
    }

    [Test]
    public void Notification_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadNotification = Notification.GetAll();

        Assert.AreEqual(1, firstLoadNotification.Count);
        Assert.AreEqual("text", firstLoadNotification.First().Text);
    }

    [Test]
    public void Customer_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadCustomer = Customer.GetAll();

        Assert.AreEqual(1, firstLoadCustomer.Count);
        Assert.AreEqual("John", firstLoadCustomer.First().FirstName);
        Assert.AreEqual("Doe", firstLoadCustomer.First().LastName);
        Assert.AreEqual("john.doe@example.com", firstLoadCustomer.First().Email);
        Assert.AreEqual("+123456789", firstLoadCustomer.First().PhoneNumber);
        Assert.AreEqual("johndoe", firstLoadCustomer.First().Login);
        Assert.AreEqual("password123", firstLoadCustomer.First().Password);
        Assert.AreEqual("street", firstLoadCustomer.First().Address);
        Assert.AreEqual("city", firstLoadCustomer.First().City);
        Assert.AreEqual(100m, firstLoadCustomer.First().WalletBalance);
    }

    [Test]
    public void CoworkingSpace_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadCoworkingSpace = CoworkingSpace.GetAll();

        Assert.AreEqual(1, firstLoadCoworkingSpace.Count);
        Assert.AreEqual("street", firstLoadCoworkingSpace.First().Address);
        Assert.AreEqual("Warsaw", firstLoadCoworkingSpace.First().City);
        Assert.AreEqual("+123456789", firstLoadCoworkingSpace.First().ContactNumber);
    }

    [Test]
    public void Coupon_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadCoupon = Coupon.GetAll();

        Assert.AreEqual(1, firstLoadCoupon.Count);
        Assert.AreEqual("DISCOUNT10", firstLoadCoupon.First().CouponCode);
        Assert.AreEqual("text", firstLoadCoupon.First().Description);
        Assert.AreEqual(10, firstLoadCoupon.First().DiscountPercentage);
    }
    [Test]
    public void BeautyProfessional_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadBeautyProfessional = BeautyProfessional.GetAll();

        Assert.AreEqual(1, firstLoadBeautyProfessional.Count);
        Assert.AreEqual("anna", firstLoadBeautyProfessional.First().FirstName);
        Assert.AreEqual("smith", firstLoadBeautyProfessional.First().LastName);
        Assert.AreEqual("anna@gmail.com", firstLoadBeautyProfessional.First().Email);
        Assert.AreEqual("+489595995", firstLoadBeautyProfessional.First().PhoneNumber);
        Assert.AreEqual("smith", firstLoadBeautyProfessional.First().Login);
        Assert.AreEqual("123", firstLoadBeautyProfessional.First().Password);
        Assert.AreEqual("street", firstLoadBeautyProfessional.First().Address);
        Assert.AreEqual("Warsaw", firstLoadBeautyProfessional.First().City);
        Assert.AreEqual(1m, firstLoadBeautyProfessional.First().WalletBalance);
        Assert.AreEqual("1 year", firstLoadBeautyProfessional.First().Experience);
        Assert.AreEqual("makeup", firstLoadBeautyProfessional.First().Specializations.First());
    }

    [Test]
    public void Booking_WriteAndLoadData_VerifyPersistency()
    {
        Repository.GetAllFromFile();

        var firstLoadBooking = Booking.GetAll();

        Assert.AreEqual(1, firstLoadBooking.Count);
        
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
    }
}
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
        var serviceBooked = new ServiceBooked(new DateTime(2025, 1, 1, 14, 30, 0));
        var service = new Service("Name", StationCategory.Body, "Description", 10m);
        var review = new Review(ReviewRating.Awful, "Comment", DateTime.Now);
        var promotion = new Promotion("Name", "Description", 10);
        var post = new Post("link", "text");
        var portfolioPage = new PortfolioPage("description");
        var payment = new Payment(10m, "C1");
        var notification = new Notification("text");
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
        var serviceBooked = ServiceBooked.GetAll().FirstOrDefault();
        serviceBooked.ServiceTime = new DateTime(2026, 1, 1, 14, 30, 0);


        var sameServiceBookedInExtent = ServiceBooked.GetAll().FirstOrDefault(s => s.Id == serviceBooked.Id);
        Assert.AreEqual(serviceBooked.ServiceTime, new DateTime(2026, 1, 1, 14, 30, 0));
        Assert.AreEqual(sameServiceBookedInExtent.ServiceTime, new DateTime(2025, 1, 1, 14, 30, 0));
    }

    [Test]
    public void ServiceBooked_NewObjectModified_ClassExtentNotModified()
    {
        var newServiceBooked =  new ServiceBooked(new DateTime(2025, 1, 1, 14, 30, 0));

        newServiceBooked.ServiceTime = new DateTime(2026, 1, 1, 14, 30, 0);
        var sameServiceBookedInExtent = ServiceBooked.GetAll().FirstOrDefault(w => w.Id == newServiceBooked.Id);
        
        
        Assert.AreEqual(newServiceBooked.ServiceTime, new DateTime(2026, 1, 1, 14, 30, 0));
        Assert.AreEqual(sameServiceBookedInExtent.ServiceTime, new DateTime(2025, 1, 1, 14, 30, 0));
    }
    
    //Review 
    [Test]
    public void Review_ObjectModified_ClassExtentNotModified()
    {
        var review = Review.GetAll().FirstOrDefault();
        review.Rating = ReviewRating.VeryGood;


        var sameReviewInExtent = Review.GetAll().FirstOrDefault(w => w.Id == review.Id);
        Assert.AreEqual(review.Rating, ReviewRating.VeryGood);
        Assert.AreEqual(sameReviewInExtent.Rating, ReviewRating.Awful);
    }

    [Test]
    public void Review_NewObjectModified_ClassExtentNotModified()
    {
        var newReview = new Review(ReviewRating.Bad, "comment", DateTime.Now.AddDays(1));
        newReview.Rating = ReviewRating.Good;
        
        var sameReviewInExtent = Review.GetAll().FirstOrDefault(w => w.Id == newReview.Id);
        
        
        Assert.AreEqual(newReview.Rating, ReviewRating.Good);
        Assert.AreEqual(sameReviewInExtent.Rating, ReviewRating.Bad);
        
    }
    
    
    //Service 
    [Test]
    public void Service_ObjectModified_ClassExtentNotModified()
    {
        var service = Service.GetAll().FirstOrDefault();
        service.ServiceCategory = StationCategory.Hair;


        var sameServiceInExtent = Service.GetAll().FirstOrDefault(w => w.Id == service.Id);
        Assert.AreEqual(service.ServiceCategory, StationCategory.Hair);
        Assert.AreEqual(sameServiceInExtent.ServiceCategory, StationCategory.Body);
    }

    [Test]
    public void Service_NewObjectModified_ClassExtentNotModified()
    {
        var newService = new Service("NewService", StationCategory.Makeup, "description", 10m);
        newService.ServiceCategory = StationCategory.Hair;
        
        var sameServiceInExtent = Service.GetAll().FirstOrDefault(w => w.Id == newService.Id);
        
        
        Assert.AreEqual(newService.ServiceCategory, StationCategory.Hair);
        Assert.AreEqual(sameServiceInExtent.ServiceCategory, StationCategory.Makeup);
    }

    //Promotion
    [Test]
    public void Promotion_ObjectModified_ClassExtentNotModified()
    {
        var promotion = Promotion.GetAll().FirstOrDefault();
        promotion.TotalDiscountPercentage = 20;


        var samePromotionInExtent = Promotion.GetAll().FirstOrDefault(w => w.Id == promotion.Id);
        Assert.AreEqual(promotion.TotalDiscountPercentage, 20);
        Assert.AreEqual(samePromotionInExtent.TotalDiscountPercentage, 10);
    }

    [Test]
    public void Promotion_NewObjectModified_ClassExtentNotModified()
    {
        var newPromotion = new Promotion("Name", "Description", 10);
        newPromotion.TotalDiscountPercentage = 20;
        
        var samePromotionInExtent = Promotion.GetAll().FirstOrDefault(w => w.Id == newPromotion.Id);
        
        
        Assert.AreEqual(newPromotion.TotalDiscountPercentage,20);
        Assert.AreEqual(samePromotionInExtent.TotalDiscountPercentage, 10);
    }



    //Post 
    [Test]
    public void Post_ObjectModified_ClassExtentNotModified()
    {
        var post = Post.GetAll().FirstOrDefault();
        post.Text = "newText";


        var samePostInExtent = Post.GetAll().FirstOrDefault(w => w.Id == post.Id);
        Assert.AreEqual(post.Text, "newText");
        Assert.AreEqual(samePostInExtent.Text, "text");
    }
    
    [Test]
    public void Post_NewObjectModified_ClassExtentNotModified()
    {
        var newPost = new Post("link", "text");
        newPost.Text = "newText";
        
        var samePostInExtent = Post.GetAll().FirstOrDefault(w => w.Id == newPost.Id);
        
        
        Assert.AreEqual(newPost.Text,"newText");
        Assert.AreEqual(samePostInExtent.Text, "text");
    }
    
    
    //PortfolioPage 
    [Test]
    public void PortfolioPage_ObjectModified_ClassExtentNotModified()
    {
        var portfolioPage = PortfolioPage.GetAll().FirstOrDefault();
        portfolioPage.Description = "newDescription";


        var samePageInExtent = PortfolioPage.GetAll().FirstOrDefault(w => w.Id == portfolioPage.Id);
        Assert.AreEqual(portfolioPage.Description, "newDescription");
        Assert.AreEqual(samePageInExtent.Description, "description");
    }
    
    [Test]
    public void PortfolioPage_NewObjectModified_ClassExtentNotModified()
    {
        var newPage = new PortfolioPage("description");
        newPage.Description = "newText";
        
        var samePageInExtent = PortfolioPage.GetAll().FirstOrDefault(w => w.Id == newPage.Id);
        
        
        Assert.AreEqual(newPage.Description,"newText");
        Assert.AreEqual(samePageInExtent.Description, "description");
    }
    
    //Payment 
    [Test]
    public void Payment_ObjectModified_ClassExtentNotModified()
    {
        var payment = Payment.GetAll().FirstOrDefault();
        payment.CouponCode = "C2";


        var samePaymentInExtent = Payment.GetAll().FirstOrDefault(w => w.Id == payment.Id);
        Assert.AreEqual(payment.CouponCode, "C2");
        Assert.AreEqual(samePaymentInExtent.CouponCode, "C1");
    }
    
    [Test]
    public void Payment_NewObjectModified_ClassExtentNotModified()
    {
        var newPayment = new Payment(10m, "C1");
        newPayment.CouponCode = "C2";
        
        var samePaymentInExtent = Payment.GetAll().FirstOrDefault(w => w.Id == newPayment.Id);
        
        
        Assert.AreEqual(newPayment.CouponCode,"C2");
        Assert.AreEqual(samePaymentInExtent.CouponCode, "C1");
    }
    
    //Notification 
    [Test]
    public void Notification_ObjectModified_ClassExtentNotModified()
    {
        
        var notification = Notification.GetAll().FirstOrDefault();
        notification.Text= "newText";


        var sameNotificationInExtent = Notification.GetAll().FirstOrDefault(w => w.Id == notification.Id);
        Assert.AreEqual(notification.Text, "newText");
        Assert.AreEqual(sameNotificationInExtent.Text, "text");
    }
    [Test]
    public void Notification_NewObjectModified_ClassExtentNotModified()
    {
        var newNotification = new Notification("text");
        newNotification.Text = "newText";
        
        var sameNotificationInExtent = Notification.GetAll().FirstOrDefault(w => w.Id == newNotification.Id);
        
        
        Assert.AreEqual(newNotification.Text,"newText");
        Assert.AreEqual(sameNotificationInExtent.Text, "text");
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
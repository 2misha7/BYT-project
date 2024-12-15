using BookingApp;
using BookingApp.Models;

namespace BookingAppTests;

//If an object is modified from the class, the class extent will be modified
public class ModificationsTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
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
        

        Repository.WriteAllToFile();
        Repository.GetAllFromFile();

    }
    //Workstation 
    [Test]
    public void Workstation_ObjectFromFileModified_ClassExtentModified()
    {
        var workstation = WorkStation.GetAll().FirstOrDefault();
        workstation.Category = StationCategory.Hair;


        var sameWorkstationInExtent = WorkStation.GetAll().FirstOrDefault(w => w.Id == workstation.Id);
        Assert.AreEqual(workstation.Category, StationCategory.Hair);
        Assert.AreEqual(sameWorkstationInExtent.Category, StationCategory.Hair);
    }
    [Test]
    public void Workstation_NewObjectModified_ClassExtentModified()
    {
        var newWorkstation = new WorkStation(StationCategory.Makeup, 15m);

        newWorkstation.Category = StationCategory.Skin;
        var sameWorkstationInExtent = WorkStation.GetAll().FirstOrDefault(w => w.Id == newWorkstation.Id);
        
        
        Assert.AreEqual(newWorkstation.Category, StationCategory.Skin);
        Assert.AreEqual(sameWorkstationInExtent.Category, StationCategory.Skin);
    }

    
    
    //Review 
    [Test]
    public void Review_ObjectModified_ClassExtentModified()
    {
        var review = Review.GetAll().FirstOrDefault();
        review.Rating = ReviewRating.VeryGood;


        var sameReviewInExtent = Review.GetAll().FirstOrDefault(w => w.Id == review.Id);
        Assert.AreEqual(review.Rating, ReviewRating.VeryGood);
        Assert.AreEqual(sameReviewInExtent.Rating, ReviewRating.VeryGood);
    }

    [Test]
    public void Review_NewObjectModified_ClassExtentModified()
    {
        var newReview = new Review(ReviewRating.Bad, "comment", DateTime.Now.AddDays(1));
        newReview.Rating = ReviewRating.Good;
        
        var sameReviewInExtent = Review.GetAll().FirstOrDefault(w => w.Id == newReview.Id);
        
        
        Assert.AreEqual(newReview.Rating, ReviewRating.Good);
        Assert.AreEqual(sameReviewInExtent.Rating, ReviewRating.Good);
        
    }
    
    
    //Service 
    [Test]
    public void Service_ObjectModified_ClassExtentModified()
    {
        var service = Service.GetAll().FirstOrDefault();
        service.ServiceCategory = StationCategory.Hair;


        var sameServiceInExtent = Service.GetAll().FirstOrDefault(w => w.Id == service.Id);
        Assert.AreEqual(service.ServiceCategory, StationCategory.Hair);
        Assert.AreEqual(sameServiceInExtent.ServiceCategory, StationCategory.Hair);
    }

    [Test]
    public void Service_NewObjectModified_ClassExtentModified()
    {
        var newService = new Service("NewService", StationCategory.Makeup, "description", 10m);
        newService.ServiceCategory = StationCategory.Hair;
        
        var sameServiceInExtent = Service.GetAll().FirstOrDefault(w => w.Id == newService.Id);
        
        
        Assert.AreEqual(newService.ServiceCategory, StationCategory.Hair);
        Assert.AreEqual(sameServiceInExtent.ServiceCategory, StationCategory.Hair);
    }

    //Promotion
    [Test]
    public void Promotion_ObjectModified_ClassExtentModified()
    {
        var promotion = Promotion.GetAll().FirstOrDefault();
        promotion.TotalDiscountPercentage = 20;


        var samePromotionInExtent = Promotion.GetAll().FirstOrDefault(w => w.Id == promotion.Id);
        Assert.AreEqual(promotion.TotalDiscountPercentage, 20);
        Assert.AreEqual(samePromotionInExtent.TotalDiscountPercentage, 20);
    }

    [Test]
    public void Promotion_NewObjectModified_ClassExtentModified()
    {
        var newPromotion = new Promotion("Name", "Description", 10);
        newPromotion.TotalDiscountPercentage = 20;
        
        var samePromotionInExtent = Promotion.GetAll().FirstOrDefault(w => w.Id == newPromotion.Id);
        
        
        Assert.AreEqual(newPromotion.TotalDiscountPercentage,20);
        Assert.AreEqual(samePromotionInExtent.TotalDiscountPercentage, 20);
    }



    //Post 
    [Test]
    public void Post_ObjectModified_ClassExtentModified()
    {
        var post = Post.GetAll().FirstOrDefault();
        post.Text = "newText";


        var samePostInExtent = Post.GetAll().FirstOrDefault(w => w.Id == post.Id);
        Assert.AreEqual(post.Text, "newText");
        Assert.AreEqual(samePostInExtent.Text, "newText");
    }
    
    [Test]
    public void Post_NewObjectModified_ClassExtentModified()
    {
        var newPost = new Post("link", "text");
        newPost.Text = "newText";
        
        var samePostInExtent = Post.GetAll().FirstOrDefault(w => w.Id == newPost.Id);
        
        
        Assert.AreEqual(newPost.Text,"newText");
        Assert.AreEqual(samePostInExtent.Text, "newText");
    }
    
    
    //PortfolioPage 
    [Test]
    public void PortfolioPage_ObjectModified_ClassExtentModified()
    {
        var portfolioPage = PortfolioPage.GetAll().FirstOrDefault();
        portfolioPage.Description = "newDescription";


        var samePageInExtent = PortfolioPage.GetAll().FirstOrDefault(w => w.Id == portfolioPage.Id);
        Assert.AreEqual(portfolioPage.Description, "newDescription");
        Assert.AreEqual(samePageInExtent.Description, "newDescription");
    }
    
    [Test]
    public void PortfolioPage_NewObjectModified_ClassExtentModified()
    {
        var newPage = new PortfolioPage("description");
        newPage.Description = "newText";
        
        var samePageInExtent = PortfolioPage.GetAll().FirstOrDefault(w => w.Id == newPage.Id);
        
        
        Assert.AreEqual(newPage.Description,"newText");
        Assert.AreEqual(samePageInExtent.Description, "newText");
    }
    
    //Payment 
    [Test]
    public void Payment_ObjectModified_ClassExtentModified()
    {
        var payment = Payment.GetAll().FirstOrDefault();
        payment.CouponCode = "C2";


        var samePaymentInExtent = Payment.GetAll().FirstOrDefault(w => w.Id == payment.Id);
        Assert.AreEqual(payment.CouponCode, "C2");
        Assert.AreEqual(samePaymentInExtent.CouponCode, "C2");
    }
    
    [Test]
    public void Payment_NewObjectModified_ClassExtentModified()
    {
        var newPayment = new Payment(10m, "C1");
        newPayment.CouponCode = "C2";
        
        var samePaymentInExtent = Payment.GetAll().FirstOrDefault(w => w.Id == newPayment.Id);
        
        
        Assert.AreEqual(newPayment.CouponCode,"C2");
        Assert.AreEqual(samePaymentInExtent.CouponCode, "C2");
    }
    
    //Notification 
    [Test]
    public void Notification_ObjectModified_ClassExtentModified()
    {
        
        var notification = Notification.GetAll().FirstOrDefault();
        notification.Text= "newText";


        var sameNotificationInExtent = Notification.GetAll().FirstOrDefault(w => w.Id == notification.Id);
        Assert.AreEqual(notification.Text, "newText");
        Assert.AreEqual(sameNotificationInExtent.Text, "newText");
    }
    [Test]
    public void Notification_NewObjectModified_ClassExtentModified()
    {
        var newNotification = new Notification("text");
        newNotification.Text = "newText";
        
        var sameNotificationInExtent = Notification.GetAll().FirstOrDefault(w => w.Id == newNotification.Id);
        
        
        Assert.AreEqual(newNotification.Text,"newText");
        Assert.AreEqual(sameNotificationInExtent.Text, "newText");
    }
    
    //Customer 
    [Test]
    public void Customer_ObjectModified_ClassExtentModified()
    {
        var customer = Customer.GetAll().FirstOrDefault();
        customer.FirstName = "NewFirstName";

        var sameCustomerInExtent = Customer.GetAll().FirstOrDefault(c => c.Id == customer.Id);

        Assert.AreEqual(customer.FirstName, "NewFirstName");
        Assert.AreEqual(sameCustomerInExtent.FirstName, "NewFirstName");
    }

    [Test]
    public void Customer_NewObjectModified_ClassExtentModified()
    {
        var newCustomer = new Customer("Bob","Ross", "bob.ross@example.com", "+123456789", "bobross", "happytrees",
            "Bob Street", "New York", 100m, new RegularAccountType()
        );
    
        newCustomer.FirstName = "ModifiedBob";
    
        var sameCustomerInExtent = Customer.GetAll().FirstOrDefault(c => c.Id == newCustomer.Id);
    
        Assert.AreEqual(newCustomer.FirstName, "ModifiedBob");
        Assert.AreEqual(sameCustomerInExtent.FirstName, "ModifiedBob");
        
    }

    
    //CoworkingSpace 
    [Test]
    public void CoworkingSpace_ObjectModified_ClassExtentModified()
    {
        var coworkingSpace = CoworkingSpace.GetAll().FirstOrDefault();
        coworkingSpace.Address= "newAddress";


        var sameCoworkingSpaceInExtent = CoworkingSpace.GetAll().FirstOrDefault(c => c.Id == coworkingSpace.Id);
        Assert.AreEqual(coworkingSpace.Address, "newAddress");
        Assert.AreEqual(sameCoworkingSpaceInExtent.Address, "newAddress");
        
    }

    [Test]
    public void CoworkingSpace_NewObjectModified_ClassExtentModified()
    {
        var newCoworkingSpace = new CoworkingSpace("Main", "New York", "+1234566789");
        newCoworkingSpace.Address = "newMain";

        var sameCoworkingSpaceInExtent = CoworkingSpace.GetAll().FirstOrDefault(c => c.Id == newCoworkingSpace.Id);

        Assert.AreEqual(newCoworkingSpace.Address, "newMain");
        Assert.AreEqual(sameCoworkingSpaceInExtent.Address, "newMain"); 
    }
    
    
    //Coupon 
    [Test]
    public void Coupon_ObjectModified_ClassExtentModified()
    {
        var coupon = Coupon.GetAll().FirstOrDefault();
        coupon.Description = "New Description";

        var sameCouponInExtent = Coupon.GetAll().FirstOrDefault(c => c.Id == coupon.Id);

        Assert.AreEqual(coupon.Description, "New Description");
        Assert.AreEqual(sameCouponInExtent.Description, "New Description");
        
        
    }
    
    [Test]
    public void Coupon_NewObjectModified_ClassExtentModified()
    {
        var newCoupon = new Coupon("NewCoupon", "Text",20,DateTime.Now, DateTime.Now.AddHours(1));
        newCoupon.Description = "newText";

        var sameCouponInExtent = Coupon.GetAll().FirstOrDefault(c => c.Id == newCoupon.Id);

        Assert.AreEqual(newCoupon.Description, "newText");
        Assert.AreEqual(sameCouponInExtent.Description, "newText"); 
        
        
    }
    
    
    //Booking 
    [Test]
    public void Booking_ObjectModified_ClassExtentModified()
    {
        var booking = new Booking();
        var sameBookingInExtent = Booking.GetAll().FirstOrDefault(b => b.Id == booking.Id);

        Assert.AreEqual(booking.Id, sameBookingInExtent.Id);
        Assert.AreEqual(booking.TotalPrice, sameBookingInExtent.TotalPrice);
        
    }
    
    
    
    
    //BeautyProfessional 
    [Test]
    public void BeautyProfessional_ObjectModified_ClassExtentModified()
    {
        var beautyProfessional = BeautyProfessional.GetAll().FirstOrDefault();
        beautyProfessional.FirstName = "ModifiedName";

        var sameBeautyProfessionalInExtent = BeautyProfessional.GetAll().FirstOrDefault(bp => bp.Id == beautyProfessional.Id);

        Assert.AreEqual(beautyProfessional.FirstName, "ModifiedName");
        Assert.AreEqual(sameBeautyProfessionalInExtent.FirstName, "ModifiedName");

    }
    
    [Test]
    public void BeautyProfessional_NewObjectModified_ClassExtentModified()
    {
        var newBeautyProfessional = new BeautyProfessional("John", "Doe", "john@google.com", "+11234567890", "johndoe", 
            "password", "Street 1", "Paris", 50m, "3 years", new List<string> { "hair stylist"}, new RegularAccountType());

        newBeautyProfessional.FirstName = "ModifiedJohn";

        var sameBeautyProfessionalInExtent = BeautyProfessional.GetAll().FirstOrDefault(bp => bp.Id == newBeautyProfessional.Id);

        Assert.AreEqual(newBeautyProfessional.FirstName, "ModifiedJohn");
        Assert.AreEqual(sameBeautyProfessionalInExtent.FirstName, "ModifiedJohn"); 
    }
    [OneTimeTearDown]
    public void TearDown()
    {
        FileOperations.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testData.json");
        File.WriteAllText(FileOperations.FilePath, string.Empty);
    }
}
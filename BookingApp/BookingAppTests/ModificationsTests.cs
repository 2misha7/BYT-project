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
        var customer = Customer.GetAll().FirstOrDefault();
        customer.FirstName = "NewFirstName";

        var sameCustomerInExtent = Customer.GetAll().FirstOrDefault(c => c.Id == customer.Id);

        Assert.AreEqual(customer.FirstName, "NewFirstName");
        Assert.AreEqual(sameCustomerInExtent.FirstName, "John");
    }

    [Test]
    public void Customer_NewObjectModified_ClassExtentNotModified()
    {
        var newCustomer = new Customer("Bob","Ross", "bob.ross@example.com", "+123456789", "bobross", "happytrees",
            "Bob Street", "New York", 100m, new RegularAccountType()
        );
    
        newCustomer.FirstName = "ModifiedBob";
    
        var sameCustomerInExtent = Customer.GetAll().FirstOrDefault(c => c.Id == newCustomer.Id);
    
        Assert.AreEqual(newCustomer.FirstName, "ModifiedBob");
        Assert.AreEqual(sameCustomerInExtent.FirstName, "Bob");
        
    }

    
    //CoworkingSpace 
    [Test]
    public void CoworkingSpace_ObjectModified_ClassExtentNotModified()
    {
        var coworkingSpace = CoworkingSpace.GetAll().FirstOrDefault();
        coworkingSpace.Address= "newAddress";


        var sameCoworkingSpaceInExtent = CoworkingSpace.GetAll().FirstOrDefault(c => c.Id == coworkingSpace.Id);
        Assert.AreEqual(coworkingSpace.Address, "newAddress");
        Assert.AreEqual(sameCoworkingSpaceInExtent.Address, "street");
        
    }

    [Test]
    public void CoworkingSpace_NewObjectModified_ClassExtentNotModified()
    {
        var newCoworkingSpace = new CoworkingSpace("Main", "New York", "+1234566789");
        newCoworkingSpace.Address = "newMain";

        var sameCoworkingSpaceInExtent = CoworkingSpace.GetAll().FirstOrDefault(c => c.Id == newCoworkingSpace.Id);

        Assert.AreEqual(newCoworkingSpace.Address, "newMain");
        Assert.AreEqual(sameCoworkingSpaceInExtent.Address, "Main"); 
    }
    
    
    //Coupon 
    [Test]
    public void Coupon_ObjectModified_ClassExtentNotModified()
    {
        var coupon = Coupon.GetAll().FirstOrDefault();
        coupon.Description = "New Description";

        var sameCouponInExtent = Coupon.GetAll().FirstOrDefault(c => c.Id == coupon.Id);

        Assert.AreEqual(coupon.Description, "New Description");
        Assert.AreEqual(sameCouponInExtent.Description, "text");
        
        
    }
    
    [Test]
    public void Coupon_NewObjectModified_ClassExtentNotModified()
    {
        var newCoupon = new Coupon("NewCoupon", "Text",20,DateTime.Now, DateTime.Now.AddHours(1));
        newCoupon.Description = "newText";

        var sameCouponInExtent = Coupon.GetAll().FirstOrDefault(c => c.Id == newCoupon.Id);

        Assert.AreEqual(newCoupon.Description, "newText");
        Assert.AreEqual(sameCouponInExtent.Description, "Text"); 
        
        
    }
    
    
    //Booking 
    [Test]
    public void Booking_ObjectModified_ClassExtentNotModified()
    {
        var booking = new Booking();
        var sameBookingInExtent = Booking.GetAll().FirstOrDefault(b => b.Id == booking.Id);

        Assert.AreEqual(booking.Id, sameBookingInExtent.Id);
        Assert.AreEqual(booking.TotalPrice, sameBookingInExtent.TotalPrice);
        
    }
    
    
    
    
    //BeautyProfessional 
    [Test]
    public void BeautyProfessional_ObjectModified_ClassExtentNotModified()
    {
        var beautyProfessional = BeautyProfessional.GetAll().FirstOrDefault();
        beautyProfessional.FirstName = "ModifiedName";

        var sameBeautyProfessionalInExtent = BeautyProfessional.GetAll().FirstOrDefault(bp => bp.Id == beautyProfessional.Id);

        Assert.AreEqual(beautyProfessional.FirstName, "ModifiedName");
        Assert.AreEqual(sameBeautyProfessionalInExtent.FirstName, "anna");

    }
    
    [Test]
    public void BeautyProfessional_NewObjectModified_ClassExtentNotModified()
    {
        var newBeautyProfessional = new BeautyProfessional("John", "Doe", "john@google.com", "+11234567890", "johndoe", 
            "password", "Street 1", "Paris", 50m, "3 years", new List<string> { "hair stylist"}, new RegularAccountType());

        newBeautyProfessional.FirstName = "ModifiedJohn";

        var sameBeautyProfessionalInExtent = BeautyProfessional.GetAll().FirstOrDefault(bp => bp.Id == newBeautyProfessional.Id);

        Assert.AreEqual(newBeautyProfessional.FirstName, "ModifiedJohn");
        Assert.AreEqual(sameBeautyProfessionalInExtent.FirstName, "John"); 
    }

}
using BookingApp.Models;

namespace BookingApp;

public class Repository
{
    public static void WriteAllToFile()
    {
        var data = new Dictionary<string, object>
        {
            {nameof(BeautyProfessional), BeautyProfessional.GetAll()},
            {nameof(Booking), Booking.GetAll()}, 
            {nameof(Coupon), Coupon.GetAll()}, 
            {nameof(CoworkingSpace), CoworkingSpace.GetAll()}, 
            {nameof(Customer), Customer.GetAll()}, 
            {nameof(Notification), Notification.GetAll()}, 
            {nameof(Payment), Payment.GetAll()}, 
            {nameof(PortfolioPage), PortfolioPage.GetAll()}, 
            {nameof(Post), Post.GetAll()}, 
            {nameof(Promotion), Promotion.GetAll()}, 
            {nameof(Review), Review.GetAll()}, 
            {nameof(Service), Service.GetAll()},
            {nameof(ServicePromoted), ServicePromoted.GetAll()}, 
            {nameof(WorkStation), WorkStation.GetAll()}, 
        };
        FileOperations.WriteToFile(data);
    }
    
    public static void GetAllFromFile()
    {
        try
        {
            BeautyProfessional.LoadClassExtentFromFile();
            Booking.LoadClassExtentFromFile();
            Coupon.LoadClassExtentFromFile();
            CoworkingSpace.LoadClassExtentFromFile();
            Customer.LoadClassExtentFromFile();
            Notification.LoadClassExtentFromFile();
            Payment.LoadClassExtentFromFile();
            PortfolioPage.LoadClassExtentFromFile();
            Post.LoadClassExtentFromFile();
            Promotion.LoadClassExtentFromFile();
            Review.LoadClassExtentFromFile();
            Service.LoadClassExtentFromFile();
            ServicePromoted.LoadClassExtentFromFile();
            WorkStation.LoadClassExtentFromFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading all entities: {ex.Message}");
        }
    }
}
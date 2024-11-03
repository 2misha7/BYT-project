using BookingApp.Models;

namespace BookingApp;

public class Repository
{
    public static void WriteAllToFile()
    {
        var data = new Dictionary<string, object>
        {
            {nameof(BeautyProfessional), BeautyProfessional.All()},
            {nameof(Booking), Booking.All()}, 
            {nameof(Coupon), Coupon.All()}, 
            {nameof(CoworkingSpace), CoworkingSpace.All()}, 
            {nameof(Customer), Customer.All()}, 
            {nameof(Notification), Notification.All()}, 
            {nameof(Payment), Payment.All()}, 
            {nameof(PortfolioPage), PortfolioPage.All()}, 
            {nameof(Post), Post.All()}, 
            {nameof(Promotion), Promotion.All()}, 
            {nameof(Review), Review.All()}, 
            {nameof(Service), Service.All()},
            {nameof(ServiceBooked), ServiceBooked.All()}, 
            {nameof(WorkStation), WorkStation.All()}, 
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
            ServiceBooked.LoadClassExtentFromFile();
            WorkStation.LoadClassExtentFromFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading all entities: {ex.Message}");
        }
    }
}
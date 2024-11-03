using System.Text.Json;
using BookingApp.Models;

namespace BookingApp;

public static class FileOperations
{
    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data.json");

    public static void SaveAll()
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
        var options = new JsonSerializerOptions
        {
            Converters = { new AccountTypeConverter() },
            WriteIndented = true 
        };
        
        try
        { 
            File.WriteAllText(FilePath, JsonSerializer.Serialize(data, options));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }


    public static void LoadAll()
    {
        try
        {
            BeautyProfessional.LoadFromFile();
            Booking.LoadFromFile();
            Coupon.LoadFromFile();
            CoworkingSpace.LoadFromFile();
            Customer.LoadFromFile();
            Notification.LoadFromFile();
            Payment.LoadFromFile();
            PortfolioPage.LoadFromFile();
            Post.LoadFromFile();
            Promotion.LoadFromFile();
            Review.LoadFromFile();
            Service.LoadFromFile();
            ServiceBooked.LoadFromFile();
            WorkStation.LoadFromFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading all entities: {ex.Message}");
        }
    }
    
}
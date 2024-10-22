namespace BookingApp.Repositories;

public class NotificationsRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "notification.json";  
}
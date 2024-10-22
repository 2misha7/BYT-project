using BookingApp.Models;

namespace BookingApp.Repositories;

public class NotificationsRepository() : AbstractRepository<Notification>(_filePath)
{
    private static readonly string _filePath = "notification.json";  
}
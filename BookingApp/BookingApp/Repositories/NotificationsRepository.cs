using BookingApp.Models;

namespace BookingApp.Repositories;

public class NotificationsRepository() : AbstractRepository<Notification>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\notification.json");
}
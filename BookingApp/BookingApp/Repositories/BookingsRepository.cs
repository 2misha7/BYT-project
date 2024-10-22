namespace BookingApp.Repositories;

public class BookingsRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "booking.json";
}


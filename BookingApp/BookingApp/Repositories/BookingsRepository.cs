using BookingApp.Models;

namespace BookingApp.Repositories;

public class BookingsRepository() : AbstractRepository<Booking>(_filePath)
{
    private static readonly string _filePath = "booking.json";
}


using BookingApp.Models;

namespace BookingApp.Repositories;

public class ServiceBookedRepository() : AbstractRepository<ServiceBooked>(_filePath)
{
    private static readonly string _filePath = "serviceBooked.json";
}
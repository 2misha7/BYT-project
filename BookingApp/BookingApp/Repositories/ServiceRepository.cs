using BookingApp.Models;

namespace BookingApp.Repositories;

public class ServiceRepository() : AbstractRepository<Service>(_filePath)
{
    private static readonly string _filePath = "C:\\Users\\HARDPC\\RiderProjects\\BYT-project\\BookingApp\\BookingApp\\Repositories\\Files\\service.json";
}
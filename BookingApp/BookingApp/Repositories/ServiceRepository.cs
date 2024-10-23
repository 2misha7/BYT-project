using BookingApp.Models;

namespace BookingApp.Repositories;

public class ServiceRepository() : AbstractRepository<Service>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\service.json");
}
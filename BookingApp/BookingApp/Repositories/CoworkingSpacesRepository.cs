using BookingApp.Models;

namespace BookingApp.Repositories;

public class CoworkingSpacesRepository() : AbstractRepository<CoworkingSpace>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\coworkingSpace.json");
}
using BookingApp.Models;

namespace BookingApp.Repositories;

public class CoworkingSpacesRepository() : AbstractRepository<CoworkingSpace>(_filePath)
{
    private const string _filePath = "coworkingSpace.json";
}
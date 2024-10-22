namespace BookingApp.Repositories;

public class CoworkingSpacesRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "coworkingSpace.json";
}
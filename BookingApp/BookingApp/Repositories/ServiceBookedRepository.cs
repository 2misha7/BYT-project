namespace BookingApp.Repositories;

public class ServiceBookedRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "serviceBooked.json";
}
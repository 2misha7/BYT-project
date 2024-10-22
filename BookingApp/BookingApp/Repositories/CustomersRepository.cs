namespace BookingApp.Repositories;

public class CustomersRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "customers.json";
}
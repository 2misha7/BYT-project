using BookingApp.Models;

namespace BookingApp.Repositories;

public class CustomersRepository() : AbstractRepository<Customer>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\customer.json");
}
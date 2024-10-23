using BookingApp.Models;

namespace BookingApp.Repositories;

public class PaymentsRepository() : AbstractRepository<Payment>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\payment.json");
}
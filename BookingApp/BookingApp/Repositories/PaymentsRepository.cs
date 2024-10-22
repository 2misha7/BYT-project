using BookingApp.Models;

namespace BookingApp.Repositories;

public class PaymentsRepository() : AbstractRepository<Payment>(_filePath)
{
    private static readonly string _filePath = "payments.json"; 
}
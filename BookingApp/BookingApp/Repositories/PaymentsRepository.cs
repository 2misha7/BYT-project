namespace BookingApp.Repositories;

public class PaymentsRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "payments.json"; 
}
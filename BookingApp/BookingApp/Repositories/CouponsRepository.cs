namespace BookingApp.Repositories;

public class CouponsRepository() : AbstractRepository(_filePath)
{
    private static readonly string _filePath = "coupon.json";
}
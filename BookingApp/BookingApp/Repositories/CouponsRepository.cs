using BookingApp.Models;

namespace BookingApp.Repositories;

public class CouponsRepository() : AbstractRepository<Coupon>(_filePath)
{
    private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\coupon.json");
}
using BookingApp.Models;

namespace BookingApp.Repositories;

public class CouponsRepository() : AbstractRepository<Coupon>(_filePath)
{
    private static readonly string _filePath = "D:\\BYT-project\\BookingApp\\BookingApp\\Repositories\\Files\\coupon.json";
}
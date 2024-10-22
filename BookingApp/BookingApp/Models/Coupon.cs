namespace BookingApp.Models;

public class Coupon(string couponCode, string description, int discountPercentage, DateTime validFrom, DateTime validTo)
{
    public string CouponCode { get; set; } = couponCode;
    public string Description { get; set; } = description;
    public int DiscountPercentage { get; set; } = discountPercentage;
    public DateTime ValidFrom { get; set; } = validFrom;
    public DateTime ValidTo { get; set; } = validTo;
}
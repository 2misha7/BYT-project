namespace ConsoleApp1.Models;

public class Coupon(int id, string couponCode, string description, int discountPercentage, DateTime validFrom, DateTime validTo) : ModelBase<Coupon> 
{
    public int Id { get; set; } = id;
    public string CouponCode { get; set; } = couponCode;
    public string Description { get; set; } = description;
    public int DiscountPercentage { get; set; } = discountPercentage;
    public DateTime ValidFrom { get; set; } = validFrom;
    public DateTime ValidTo { get; set; } = validTo;
}
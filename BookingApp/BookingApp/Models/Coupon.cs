namespace BookingApp.Models;

public class Coupon(string code, string description, int discountPercentage, DateTime validFrom, DateTime validTo)
{
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
    public int DiscountPercentage { get; set; } = discountPercentage;
    public DateTime ValidFrom { get; set; } = validFrom;
    public DateTime ValidTo { get; set; } = validTo;
}
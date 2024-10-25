namespace ConsoleApp1.Models;

public class Payment(int id, string? couponCode, decimal finalAmount, decimal amountPaid) : ModelBase<Payment>
{
    public int Id { get; set; } = id;
    public string? CouponCode { get; set; } = couponCode;
    public decimal FinalAmount { get; set; } = finalAmount;
    public decimal AmountPaid { get; set; } = amountPaid;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
}
public enum PaymentStatus
{
    Pending, 
    Completed,
    Failed,
    Cancelled,
    Refunded
}
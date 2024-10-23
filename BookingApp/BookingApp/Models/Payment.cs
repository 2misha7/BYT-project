using BookingApp.Models.Enums;

namespace BookingApp.Models;

public class Payment(Guid bookingId, string? couponCode, decimal finalAmount, decimal amountPaid) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? CouponCode { get; set; } = couponCode;
    public decimal FinalAmount { get; set; } = finalAmount;
    public decimal AmountPaid { get; set; } = amountPaid;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public Guid BookingId { get; set; } = bookingId;

}
public enum PaymentStatus
{
    Pending, 
    Completed,
    Failed,
    Cancelled,
    Refunded
}
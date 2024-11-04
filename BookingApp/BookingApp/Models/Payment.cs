namespace BookingApp.Models;

public class Payment : ModelBase<Payment>
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private string? _couponCode;

    public string? CouponCode
    {
        get => _couponCode;
        set
        {
            if (value != null && string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Coupon code cannot be empty or whitespace.");
            }
            _couponCode = value;
        }

    }

   
    private decimal _finalAmount;
    public decimal FinalAmount
    {
        get => _finalAmount;
        private set 
        {
            if (value < 0)
            {
                throw new ArgumentException("Final amount cannot be negative");
            }
            _finalAmount = value;
        }
    }

    private decimal _amountPaid;
    public decimal AmountPaid
    {
        get => _amountPaid;
        private set 
        {
            if (value < 0)
            {
                throw new ArgumentException("Amount paid cannot be negative");
            }
            _amountPaid = value;
        }
    }

    private PaymentStatus _status;

    public PaymentStatus Status
    {
        get => _status;
        set => _status = value;
    }
    
    public Payment(decimal finalAmount, string? couponCode)
    {
        try
        {
            FinalAmount = finalAmount;
            CouponCode = couponCode;
            AmountPaid = 0; 
            Status = PaymentStatus.Pending; 
            Add(new Payment(this));
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    

    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }

    private Payment(Payment original)
    {
        _id = original._id;
        _couponCode = original._couponCode;
        _finalAmount = original._finalAmount;
        _amountPaid = original._amountPaid;
        _status = original._status;
    }

    // Clone method
    protected override Payment Clone()
    {
        return new Payment(this);
    }
}
public enum PaymentStatus
{
    Pending, 
    Completed,
    Failed,
    Cancelled,
    Refunded
}
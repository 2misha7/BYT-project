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
            Add(this);
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    public Payment(decimal finalAmount, string? couponCode, Booking booking)
    {
        try
        {
            _booking = Booking;
            FinalAmount = finalAmount;
            CouponCode = couponCode;
            AmountPaid = 0; 
            Status = PaymentStatus.Pending; 
            _booking.AddPaymentToBooking(this);
            Add(this);
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    

    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }
    
    
    // One-to-One Relationship with Booking
    private Booking _booking;

    public Booking Booking
    {
        get => _booking;
        set => _booking = value;
    }
    private bool _isUpdating = false;
    public void AddBookingToPayment(Booking booking)
    {
        if (booking == null)
            throw new ArgumentNullException(nameof(booking));

        if (_isUpdating)
        {
            return;
        }

        if (_booking != null)
        {
            throw new InvalidOperationException("This Payment is already assigned to a Booking.");
        }

        _isUpdating = true;
        _booking = booking;
        booking.AddPaymentToBooking(this);
        _isUpdating = false;
    }
    
    public void RemoveBookingFromPayment()
    {
        if (_isUpdating) return;
        if (_booking == null) 
            throw new InvalidOperationException("This Payment does not have a booking");
        _isUpdating = true;
        var previousBooking = _booking;
        _booking= null;
        previousBooking.RemovePaymentFromBooking(); 
        _isUpdating = false;
        
    }
    
    public void ChangeBookingForThisPayment(Booking newBooking)
    {
        if (newBooking == null)
            throw new ArgumentNullException(nameof(newBooking));
        if (_booking == newBooking)
        {
            throw new InvalidOperationException("This Payment is already assigned to this Booking");
        }

        if (_booking == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new Booking to this Payment, because it does not have any");
        }
        RemoveBookingFromPayment(); 
        AddBookingToPayment(newBooking); 
    }
}




public enum PaymentStatus
{
    Pending, 
    Accepted,
    Canceled, 
    Refunded
}
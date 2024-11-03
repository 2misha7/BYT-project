namespace BookingApp.Models;

//check optionality of an attribute
public class Coupon : ModelBase<Coupon> 
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    private string _couponCode = null!;
    public string CouponCode
    {
        get => _couponCode;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Coupon code cannot be empty");
            }
            _couponCode = value;
        }
    }

    private string _description = null!;
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be empty");
            }
            _description = value;
        }
    }
    private int _discountPercentage;
    public int DiscountPercentage
    {
        get => _discountPercentage;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Discount percentage must be between 0 and 100");
            }
            _discountPercentage = value;
        }
    }
    private DateTime _validFrom;
    public DateTime ValidFrom
    {
        get => _validFrom;
        set
        {
            
            _validFrom = value;
        }
    }
    private DateTime _validTo;
    public DateTime ValidTo
    {
        get => _validTo;
        set
        {
            if (value < ValidFrom)
            {
                throw new ArgumentException("ValidTo date cannot be earlier than ValidFrom date");
            }
            _validTo = value;
        }
    }
    
    public Coupon(string couponCode, string description, int discountPercentage, DateTime validFrom, DateTime validTo)
    {
        try
        {
            CouponCode = couponCode;
            Description = description;
            DiscountPercentage = discountPercentage;
            ValidFrom = validFrom;
            ValidTo = validTo;
            Add(this);
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }


    }
    protected override void AssignId()
    {
        Id = All().Count > 0 ? All().Last().Id + 1 : 1; // Assign new ID
    }
}
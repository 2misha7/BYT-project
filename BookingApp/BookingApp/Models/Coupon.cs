namespace BookingApp.Models;

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
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }
    
    //Association with Customer (many-to-one)
    private Customer? _customer; 

    public Customer? Customer => _customer;
    private bool _isUpdating = false; 
    
    public void AssignToCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (_isUpdating)
        {
            return;
        }
        if (_customer != null)
        {
            throw new InvalidOperationException("This Coupon is already assigned to a Customer.");
        }
        _isUpdating = true;
        _customer = customer;
        customer.AddCoupon(this);
        _isUpdating = false;
    }
    
    public void TakeCouponFromCustomer()
    {
        if (_isUpdating) return;
        if (_customer == null) 
            throw new InvalidOperationException("This coupon is not assigned to a Customer");
        _isUpdating = true;
        var previousCustomer = _customer;
        _customer = null;
        previousCustomer.RemoveCoupon(this); 
        _isUpdating = false;
        
    }
    
    public void ChangeCustomerAssignedToCoupon(Customer newCustomer)
    {
        if (newCustomer == null)
            throw new ArgumentNullException(nameof(newCustomer));
        if (_customer == newCustomer)
        {
            throw new InvalidOperationException("This Coupon is already assigned to exactly this Customer");
        }

        if (_customer == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign this coupon to a new Customer, because it is not assigned to any");
        }
        TakeCouponFromCustomer(); 
        AssignToCustomer(newCustomer); 
    }
    
    
}

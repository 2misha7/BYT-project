namespace BookingApp.Models;

public class Booking : ModelBase<Booking>
{
    
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    //do after implementing relationships, no setter, getter must return totalPrice
    private decimal _totalPrice = 0;
    public decimal TotalPrice
    {
        get => _totalPrice;
    }
    
    public Booking()
    {
        try
        {
            Add(this);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
        
    }
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }
    //Booking Customer Association (many-to-one)
    private Customer? _customer; 
    public Customer? Customer => _customer;
    private bool _isUpdating = false; 
    public void AddBookingToCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (_isUpdating)
        {
            return;
        }
        if (_customer != null)
        {
            throw new InvalidOperationException("This Booking is already assigned to a Customer.");
        }
        _isUpdating = true;
        _customer = customer;
        customer.AddBooking(this);
        _isUpdating = false;
    }
    public void RemoveBookingFromCustomer()
    {
        if (_isUpdating) return;
        if (_customer == null) 
            throw new InvalidOperationException("This booking is not assigned to a Customer");
        _isUpdating = true;
        var previousCustomer = _customer;
        _customer = null;
        previousCustomer.RemoveBooking(this); 
        _isUpdating = false;
        
    }
    
    public void ChangeCustomerInBooking(Customer newCustomer)
    {
        if (newCustomer == null)
            throw new ArgumentNullException(nameof(newCustomer));
        if (_customer == newCustomer)
        {
            throw new InvalidOperationException("This Booking already belongs to exactly this Customer");
        }

        if (_customer == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign this booking to a new Customer, because it is not assigned to any");
        }
        RemoveBookingFromCustomer(); 
        AddBookingToCustomer(newCustomer); 
    }
}
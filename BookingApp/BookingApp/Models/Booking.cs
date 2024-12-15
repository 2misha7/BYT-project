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
    // Notification Association (One-to-One)
    private Notification? _notification;
    public Notification? Notification => _notification;

    public void AddNotificationToBooking(Notification notification)
    {
        if (notification == null)
            throw new ArgumentNullException(nameof(notification));

        if (_isUpdating)
        {
            return;
        }

        if (_notification != null)
        {
            throw new InvalidOperationException("This Booking already has a Notification.");
        }

        _isUpdating = true;
        _notification = notification;
        notification.AddBookingToNotification(this);
        _isUpdating = false;
    }
    
    public void RemoveBookingFromNotification()
    {
        if (_isUpdating) return;
        if (_notification == null) 
            throw new InvalidOperationException("This booking does not have a notification");
        _isUpdating = true;
        var previousNotification = _notification;
        _notification = null;
        previousNotification.RemoveNotificationFromBooking(); 
        _isUpdating = false;
        
    }
    
    public void ChangeNotificationInBooking(Notification newNotification)
    {
        if (newNotification == null)
            throw new ArgumentNullException(nameof(newNotification));
        if (_notification == newNotification)
        {
            throw new InvalidOperationException("This Booking already has exactly this Notification");
        }

        if (_notification == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new notification to this Booking, because it does not have any");
        }
        RemoveBookingFromNotification(); 
        AddNotificationToBooking(newNotification); 
    }
    
    // One-to-One Relationship with Payment
    private Payment? _payment;
    public Payment? Payment => _payment;
    
    public void AddPaymentToBooking(Payment payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));

        if (_isUpdating)
        {
            return;
        }

        if (_payment != null)
        {
            throw new InvalidOperationException("This Booking already has a Payment.");
        }

        _isUpdating = true;
        _payment = payment;
        payment.AddBookingToPayment(this);
        _isUpdating = false;
    }
    
    public void RemovePaymentFromBooking()
    {
        if (_isUpdating) return;
        if (_payment == null) 
            throw new InvalidOperationException("This Booking does not have a Payment");
        _isUpdating = true;
        var previousPayment = _payment;
        _payment = null;
        previousPayment.RemoveBookingFromPayment(); 
        _isUpdating = false;
        
    }
    
    public void ChangePaymentForBooking(Payment newPayment)
    {
        if (newPayment == null)
            throw new ArgumentNullException(nameof(newPayment));
        if (_payment == newPayment)
        {
            throw new InvalidOperationException("This Payment is already assigned to this Booking");
        }

        if (_payment == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new Payment to this Booking, because it does not have any");
        }
        RemovePaymentFromBooking(); 
        AddPaymentToBooking(newPayment); 
    }
    //Booking-Service (one-to-many)
    private readonly List<Service> _services = new();
    public IReadOnlyList<Service> Services => _services.AsReadOnly();
    public void AddService(Service service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (_isUpdating)
        {
            return; 
        }
        if (_services.Contains(service))
            throw new InvalidOperationException("This Booking already has this Service.");
        _isUpdating = true;
        _services.Add(service);
        service.AssignToBooking(this);
        _isUpdating = false;
    }
    public void RemoveService(Service service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        if (_isUpdating) return; 
        if (!_services.Contains(service)) throw new InvalidOperationException("This Booking does not have this Service."); 

        _isUpdating = true;
        _services.Remove(service);
        service.RemoveFromBooking();
        _isUpdating = false;
    }
    public void SubstituteService(Service oldS, Service newS)
    {
        if (oldS == null)
            throw new ArgumentNullException(nameof(oldS));
        if (newS == null)
            throw new ArgumentNullException(nameof(newS));
        if (!_services.Contains(oldS))
        {
            throw new Exception("This Booking does not have this old Service");
        }

        if (_services.Contains(newS))
        {
            throw new Exception("This Booking already had this new Service");
        }
        
        if (newS.Booking != null)
        {
            throw new Exception("It is not possible to add this Service to a Booking, as it is already assigned to a Booking in the system");
        }
        
        RemoveService(oldS); 
        AddService(newS);
    }
}
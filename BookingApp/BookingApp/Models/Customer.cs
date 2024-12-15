using System.Text.RegularExpressions;

namespace BookingApp.Models;

public class Customer : ModelBase<Customer>, IPerson
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private string _firstName = null!;
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("First name cannot be empty");
            }
            _firstName = value;
        }
    }
    private string _lastName = null!;
    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Last name cannot be empty");
            }
            _lastName = value;
        }
    }
    private string _email = null!;
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Email cannot be empty");
            }
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 

            if (!Regex.IsMatch(value, emailPattern))
            {
                throw new ArgumentException("Invalid email format");
            }
            _email = value;
        }
    }
    private string _phoneNumber = null!;

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Phone number cannot be empty");
            }
            var phonePattern = @"^\+?[1-9]\d{1,14}$"; 

            if (!Regex.IsMatch(value, phonePattern))
            {
                throw new ArgumentException("Invalid phone number format");
            }

            _phoneNumber = value;
        }
        
    }
    private string _login = null!;
    public string Login
    {
        get => _login;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Login cannot be empty");
            }
            _login = value;
        }
    }
    
    private string _password = null!;
    public string Password
    {
        get => _password;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Password cannot be empty");
            }
            _password = value; 
        }
    }
    
    private string _address = null!;
    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Address cannot be empty");
            }
            _address = value;
        }
    }
    private string _city = null!;
    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("City cannot be empty");
            }
            _city = value;
        }
    }
    private decimal _walletBalance;
    public decimal WalletBalance
    {
        get => _walletBalance;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Wallet balance cannot be negative");
            }
            _walletBalance = value;
        }
    }
    private IAccountType _accountType = null!;
    public IAccountType AccountType
    {
        get => _accountType;
        set => _accountType = value ?? throw new ArgumentException("Account type cannot be null");
    }
    
    public Customer(string firstName, string lastName, string email, string phoneNumber, string login, string password, string address, string city, decimal walletBalance, IAccountType accountType)
    {
        try
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Login = login;
            Password = password;
            Address = address;
            City = city;
            WalletBalance = walletBalance;
            AccountType = accountType;
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
    //Association with Coupon (one-to-many)
    private readonly List<Coupon> _coupons = new();
    public IReadOnlyList<Coupon> Coupons => _coupons.AsReadOnly();
    private bool _isUpdating = false;
    
    public void AddCoupon(Coupon coupon)
    {
        if (coupon == null)
            throw new ArgumentNullException(nameof(coupon));
        if (_isUpdating)
        {
            return; 
        }
        if (_coupons.Contains(coupon))
            throw new InvalidOperationException("This Customer already has this Coupon.");
        _isUpdating = true;
        _coupons.Add(coupon);
        coupon.AssignToCustomer(this);
        _isUpdating = false;
    }
    
    public void RemoveCoupon(Coupon coupon)
    {
        if (coupon == null)
            throw new ArgumentNullException(nameof(coupon));

        if (_isUpdating) return; 
        if (!_coupons.Contains(coupon)) throw new InvalidOperationException("This Customer does not have this Coupon."); 

        _isUpdating = true;
        _coupons.Remove(coupon);
        coupon.TakeCouponFromCustomer();
        _isUpdating = false;
    }
    
    public void SubstituteCoupon(Coupon oldCoupon, Coupon newCoupon)
    {
        if (oldCoupon == null)
            throw new ArgumentNullException(nameof(oldCoupon));
        if (newCoupon == null)
            throw new ArgumentNullException(nameof(newCoupon));
        if (!_coupons.Contains(oldCoupon))
        {
            throw new Exception("This Customer does not have this old Coupon");
        }

        if (_coupons.Contains(newCoupon))
        {
            throw new Exception("This Customer already had this new Coupon");
        }
        
        if (newCoupon.Customer != null)
        {
            throw new Exception("It is not possible to add this Coupon to a Customer, as it is already assigned to a Customer in the system");
        }
        
        RemoveCoupon(oldCoupon); 
        AddCoupon(newCoupon);
    }
    
    //Customer-Booking Association  (one-to-many)
    private readonly List<Booking> _bookings = new();
    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();
    public void AddBooking(Booking booking)
    {
        if (booking == null)
            throw new ArgumentNullException(nameof(booking));
        if (_isUpdating)
        {
            return; 
        }
        if (_bookings.Contains(booking))
            throw new InvalidOperationException("This Customer already has this Booking.");
        _isUpdating = true;
        _bookings.Add(booking);
        booking.AddBookingToCustomer(this);
        _isUpdating = false;
    }
    public void RemoveBooking(Booking booking)
    {
        if (booking == null)
            throw new ArgumentNullException(nameof(booking));

        if (_isUpdating) return; 
        if (!_bookings.Contains(booking)) throw new InvalidOperationException("This Customer does not have this Booking."); 

        _isUpdating = true;
        _bookings.Remove(booking);
        booking.RemoveBookingFromCustomer();
        _isUpdating = false;
    }
    
    public void SubstituteBooking(Booking oldBooking, Booking newBooking)
    {
        if (oldBooking == null)
            throw new ArgumentNullException(nameof(oldBooking));
        if (newBooking == null)
            throw new ArgumentNullException(nameof(newBooking));
        if (!_bookings.Contains(oldBooking))
        {
            throw new Exception("This Customer does not have this old Booking");
        }

        if (_bookings.Contains(newBooking))
        {
            throw new Exception("This Customer already had this new Booking");
        }
        
        if (newBooking.Customer != null)
        {
            throw new Exception("It is not possible to add this Booking to a Customer, as it is already assigned to a Customer in the system");
        }
        
        RemoveBooking(oldBooking); 
        AddBooking(newBooking);
    }
    
    //Customer-Customer Association 
    private readonly List<Customer> _invitedCustomers = new();
    public IReadOnlyList<Customer> InvitedCustomers => _invitedCustomers.AsReadOnly();
 
    private Customer? _inviter; 
    public Customer? Inviter => _inviter;
    public void AddInvitedCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (customer == this)
        {
            throw new InvalidOperationException("It is not possible for customers to invite themselves.");
        }

        if (customer._invitedCustomers.Contains(this))
        {
            throw new InvalidOperationException("It is not possible to invite a customer who has invited you.");
        }
        if (_isUpdating)
        {
            return; 
        }
        if (_invitedCustomers.Contains(customer))
            throw new InvalidOperationException("This Customer has already invited this new Customer.");
        _isUpdating = true;
        _invitedCustomers.Add(customer);
        customer.AddInviter(this);
        _isUpdating = false;
    }

    public void AddInviter(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (customer == this)
        {
            throw new InvalidOperationException("It is not possible for customers to invite themselves.");
        }
        if (this._invitedCustomers.Contains(customer))
        {
            throw new InvalidOperationException("It is not possible to be invited by a Customer who has invited you.");
        }

        if (_isUpdating)
        {
            return;
        }
        if (_inviter != null)
        {
            throw new InvalidOperationException("This Customer alread has an inviter.");
        }
        _isUpdating = true;
        _inviter = customer;
        customer.AddInvitedCustomer(this);
        _isUpdating = false;
    }
    public void RemoveInvitedCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        if (_isUpdating) return; 
        if (!_invitedCustomers.Contains(customer)) throw new InvalidOperationException("This Customer hasn't invited this new Customer."); 

        _isUpdating = true;
        _invitedCustomers.Remove(customer);
        customer.RemoveInviter();
        _isUpdating = false;
    }

    public void RemoveInviter()
    {
        if (_isUpdating) return;
        if (_inviter == null) 
            throw new InvalidOperationException("This Customer does not have an inviter");
        _isUpdating = true;
        var previousCustomer = _inviter;
        _inviter = null;
        previousCustomer.RemoveInvitedCustomer(this); 
        _isUpdating = false;
    }
    
    public void SubstituteInvitedCustomer(Customer oldC, Customer newC)
    {
        if (oldC == null)
            throw new ArgumentNullException(nameof(oldC));
        if (newC == null)
            throw new ArgumentNullException(nameof(newC));
        if (!_invitedCustomers.Contains(oldC))
        {
            throw new Exception("This Customer has not invited this old Customer");
        }

        if (_invitedCustomers.Contains(newC))
        {
            throw new Exception("This Customer has already invited this new Customer");
        }
        
        if (newC.Inviter != null)
        {
            throw new Exception("It is not possible to invite this new Customer, as this Customer has already been invited to the system");
        }
        
        RemoveInvitedCustomer(oldC); 
        AddInvitedCustomer(newC);
    }
    
    public void ChangeInviter(Customer newInviter)
    {
        if (newInviter == null)
            throw new ArgumentNullException(nameof(newInviter));
        if (_inviter == newInviter)
        {
            throw new InvalidOperationException("This Customer has already been invited by exactly this inviter");
        }

        if (_inviter == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign new inviter to this Customer, because it has not been invited to the system, by any");
        }
        RemoveInviter(); 
        AddInviter(newInviter); 
    }
}
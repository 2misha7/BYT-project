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
    
    public Customer(int id, string firstName, string lastName, string email, string phoneNumber, string login, string password, string address, string city, decimal walletBalance, IAccountType accountType)
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
}
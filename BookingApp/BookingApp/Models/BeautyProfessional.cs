using System.Text.RegularExpressions;

namespace BookingApp.Models;

public class BeautyProfessional :  ModelBase<BeautyProfessional>, IPerson
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
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

    private string _experience = null!;
    public string Experience
    {
        get => _experience;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Experience cannot be empty");
            }
            _experience = value;
        }
    }
    private ICollection<string> _specializations = null!;
    //todo empty string
    public ICollection<string> Specializations
    {
        get => _specializations;
        set
        {
            if (value == null || value.Count == 0)
            {
                throw new ArgumentException("Specializations cannot be empty");
            }
            _specializations = value;
        }
    }
    private IAccountType _accountType = null!;
    public IAccountType AccountType
    {
        get => _accountType;
        set => _accountType = value ?? throw new ArgumentException("Account type cannot be null");
    }
    
    public BeautyProfessional(string firstName, string lastName, string email, string phoneNumber, string login, string password, string address, string city, decimal walletBalance, string experience, ICollection<string> specializations, IAccountType accountType)
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
            Experience = experience;
            Specializations = specializations;
            AccountType = accountType;
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
    
    // One-to-One Relationship BeautyPro - PortfolioPage
    private PortfolioPage? _portfolioPage;
    public PortfolioPage? PortfolioPage => _portfolioPage;
    private bool _isUpdating = false;
    
    public void AddPortfolioPageToBeautyPro(PortfolioPage portfolioPage)
    {
        if (portfolioPage == null)
            throw new ArgumentNullException(nameof(portfolioPage));

        if (_isUpdating)
        {
            return;
        }

        if (_portfolioPage != null)
        {
            throw new InvalidOperationException("This Beauty Pro already has a PortfolioPage.");
        }

        _isUpdating = true;
        _portfolioPage = portfolioPage;
        portfolioPage.AddBeautyProToPortfolioPage(this);
        _isUpdating = false;
    }
    
    public void RemovePortfolioPageFromBeautyPro()
    {
        if (_isUpdating) return;
        if (_portfolioPage == null) 
            throw new InvalidOperationException("This BeautyPro does not have a PortfolioPage");
        _isUpdating = true;
        var previousPortfolioPage = _portfolioPage;
        _portfolioPage = null;
        previousPortfolioPage.RemoveBeautyProFromPortfolioPage(); 
        _isUpdating = false;
        
    }
    
    public void ChangePortfolioPageForBeautyPro(PortfolioPage newPortfolioPage)
    {
        if (newPortfolioPage == null)
            throw new ArgumentNullException(nameof(newPortfolioPage));
        if (_portfolioPage == newPortfolioPage)
        {
            throw new InvalidOperationException("This PortfolioPage is already assigned to this BeautyPro");
        }

        if (_portfolioPage == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new PortfolioPage to this BeautyPro, because it does not have any");
        }
        RemovePortfolioPageFromBeautyPro(); 
        AddPortfolioPageToBeautyPro(newPortfolioPage); 
    }
    
    //BeautyProfessional-Service (many-to-one)
    private readonly List<Service> _services = new();
    public IReadOnlyList<Service> Services => _services.AsReadOnly(); 
    public void AddServiceToBeautyProfessional(Service service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (_isUpdating)
        {
            return; 
        }
        if (_services.Contains(service))
            throw new InvalidOperationException("This BeautyPro already has this Service.");
        _isUpdating = true;
        _services.Add(service);
        service.AddBeautyProToService(this);
        _isUpdating = false;
    }
    public void RemoveServiceFromBeautyPro(Service service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));

        if (_isUpdating) return; 
        if (!_services.Contains(service)) throw new InvalidOperationException("This BeautyPro does not have this Service."); 

        _isUpdating = true;
        _services.Remove(service);
        service.RemoveBeautyFromService();
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
            throw new Exception("This BeautyPro does not have this old Service");
        }

        if (_services.Contains(newS))
        {
            throw new Exception("This BeautyPro already had this new Service");
        }
        
        if (newS.BeautyProfessional != null)
        {
            throw new Exception("It is not possible to add this new Service to a BeautyPro, as it is already assigned to a BeautyPro in the system");
        }
        
        RemoveServiceFromBeautyPro(oldS); 
        AddServiceToBeautyProfessional(newS);
    }
}


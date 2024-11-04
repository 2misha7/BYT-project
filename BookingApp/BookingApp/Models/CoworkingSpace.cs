namespace BookingApp.Models;

public class CoworkingSpace : ModelBase<CoworkingSpace>
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
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
    private string _contactNumber = null!;
    public string ContactNumber
    {
        get => _contactNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Contact number cannot be empty");
            }
            var phonePattern = @"^\+?[1-9]\d{1,14}$"; 
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, phonePattern))
            {
                throw new ArgumentException("Invalid contact number format");
            }
            _contactNumber = value;
        }
    }

    public CoworkingSpace(string address, string city, string contactNumber)
    {
        try
        {
            AssignId();
            Address = address;
            City = city;
            ContactNumber = contactNumber;
            Add(new CoworkingSpace(this));
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }

    private CoworkingSpace(CoworkingSpace original)
    {
        _id = original._id;
        _address = original._address;
        _city = original._city;
        _contactNumber = original._contactNumber;
    }

    // Clone method
    protected override CoworkingSpace Clone()
    {
        return new CoworkingSpace(this);
    }
}
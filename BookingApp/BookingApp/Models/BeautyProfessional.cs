namespace ConsoleApp1.Models;

public class BeautyProfessional :  ModelBase<BeautyProfessional>, IPerson
{
    public BeautyProfessional(int id, string firstName, string lastName, string email, string phoneNumber, string login, string password, string address, string city, decimal walletBalance, string experience, ICollection<string> specializations, IAccountType accountType)
    {
        Id = id;
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
    
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public decimal WalletBalance { get; set; }
    public string Experience { get; set; } 
    public ICollection<string> Specializations { get; set; } 
    public IAccountType AccountType { get; set; } 
}
namespace BookingApp.Models;

public abstract class Person : IEntity
{
    protected Person(string firstName, string lastName, string email, string phoneNumber, string login, string password, string address, string city, decimal walletBalance) 
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Login = login;
        Password = password;
        Address = address;
        City = city;
        WalletBalance = 0;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public decimal WalletBalance { get; set; }
    
}
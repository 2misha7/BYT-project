namespace ConsoleApp1.Models;

public class Customer(
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    string login,
    string password,
    string address,
    string city,
    decimal walletBalance,
    IAccountType accountType)
    : ModelBase<Customer>, IPerson
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
    public string Address { get; set; } = address;
    public string City { get; set; } = city;
    public decimal WalletBalance { get; set; } = walletBalance;
    public IAccountType AccountType { get; set; } = accountType;
}
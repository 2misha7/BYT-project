namespace BookingApp.Models;

public interface IPerson
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    string Address { get; set; }
    string City { get; set; }
    decimal WalletBalance { get; set; }
}
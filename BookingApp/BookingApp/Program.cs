using System.IO.Pipes;
using BookingApp;
using BookingApp.Models;



var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
var customer2 = new Customer("Jack", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
var customer3 = new Customer("777", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());

customer.AddInviter(customer2);
Console.WriteLine(customer.Inviter.FirstName);
Console.WriteLine(customer2.InvitedCustomers.First().FirstName);
customer2.SubstituteInvitedCustomer(customer, customer3);
Console.WriteLine(customer.Inviter.FirstName);
Console.WriteLine(customer2.InvitedCustomers.First().FirstName);
//customer.RemoveInviter();
//Console.WriteLine(customer.Inviter.FirstName);
//Console.WriteLine(customer2.InvitedCustomers.First().FirstName);

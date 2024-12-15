using System.IO.Pipes;
using BookingApp;
using BookingApp.Models;



//var customer = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
//var customer2 = new Customer("Jack", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
//var customer3 = new Customer("777", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
//
//customer.AddInviter(customer2);
//Console.WriteLine(customer.Inviter.FirstName);
//Console.WriteLine(customer2.InvitedCustomers.First().FirstName);
//customer2.SubstituteInvitedCustomer(customer, customer3);
//Console.WriteLine(customer.Inviter.FirstName);
//Console.WriteLine(customer2.InvitedCustomers.First().FirstName);
//customer.RemoveInviter();
//Console.WriteLine(customer.Inviter.FirstName);
//Console.WriteLine(customer2.InvitedCustomers.First().FirstName);
//var service = new Service("asfas", StationCategory.Body, "sadas", 21);
//var promotion = new Promotion("asdasdsa", "adassd", 15);
//service.AddPromotion(promotion, DateTime.Now, DateTime.Now.AddDays(4), null);
//service.AddPromotion(promotion, DateTime.Now, DateTime.Now.AddDays(4), null);

//service.RemovePromotion(promotion);


//Console.WriteLine(promotion.ServicePromotions.First().Promotion.Name);
//Console.WriteLine(service.ServicePromotions.First().Promotion.Name);
//Console.WriteLine(service.ServicePromotions.First().Service.Name);
//
//Console.WriteLine(promotion.ServicePromotions.First().Promotion.Name);
//Console.WriteLine(promotion.ServicePromotions.First().Service.Name);
//
//Console.WriteLine(ServicePromoted.GetAll().First().Promotion.Name);
//Console.WriteLine(ServicePromoted.GetAll().First().Service.Name);
//Console.WriteLine(ServicePromoted.GetAll().Count);
var service = new Service("NewService", StationCategory.Body, "New Description", 15);
var workstation = new WorkStation(StationCategory.Body, 45);
workstation.AddServiceAtTime(service, new DateTime(2024,12,01));
//In any case will pe printed to Console
Console.WriteLine(service.AssignedWorkStation.Price);
workstation.RemoveServiceAtTime(service);
//If incorrect method used: KeyNotFoundException will be thrown 
if (service.AssignedWorkStation == null)
{
    Console.WriteLine("Workstation removed succesfully");
}


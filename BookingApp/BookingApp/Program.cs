using System.Security.AccessControl;
using BookingApp.Models;
using BookingApp.Models.Enums;
using BookingApp.Repositories;

var customer = new Customer("John", "Smith", "john@qw.wq", "123", "qwe", "qwe34", "wqr", "wasd", new PremiumAccountType(DateTime.Now, SubscriptionDuration.Year));

var repo = new CustomersRepository();
repo.Add(customer);

var beaProf = new BeautyProfessional("John", "Smith", "qwe", "123", "qwe", "qwe34", "wqr", "wasd", new List<string>(),
    "qwe", new RegularAccountType());

var repo1 = new BeautyProfessionalsRepository();
repo1.Add(beaProf);


var coworkSp = new CoworkingSpace("srfdgf", "sdfg", "waesfrd");
var repo2 = new CoworkingSpacesRepository();
repo2.Add(coworkSp);

var workstation1 = new WorkStation(StationCategory.Body, 2, coworkSp.Id);
var repo3 = new WorkStationsRepository();
repo3.Add(workstation1);


var service1 = new Service(beaProf.Id,"qwe", StationCategory.Body, "qwret", (decimal)24.35, workstation1.Id);
var service2 = new Service(beaProf.Id,"qwe", StationCategory.Body, "qwret", (decimal)27.555, workstation1.Id);
var repo4 = new ServiceRepository();
repo4.Add(service1);
repo4.Add(service2);


var servicesBooked = new List<ServiceBooked>();

var sb1 = new ServiceBooked(service1.Id, DateTime.Today);
var sb2 = new ServiceBooked(service2.Id, DateTime.Today);
servicesBooked.Add(sb1);
servicesBooked.Add(sb2);


var repo5 = new ServiceBookedRepository();
repo5.Add(sb1);
repo5.Add(sb2);

var listOfServicesBooked = new List<Guid>();
foreach (var s in servicesBooked)
{
    listOfServicesBooked.Add(s.Id);
}


var booking = new Booking(customer.Id, listOfServicesBooked);
var repoB = new BookingsRepository();
repoB.AddBooking(booking);
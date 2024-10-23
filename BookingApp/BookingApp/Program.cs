
using System.Security.AccessControl;
using BookingApp.Models;
using BookingApp.Repositories;

//var coupon = new Coupon("wsdfxcv","fddf", 12, DateTime.Today, DateTime.Today);
var repo = new CustomersRepository();

repo.Delete(Guid.Parse("61185441-101f-4bd8-b80c-11d9932116e4"));

var list =  repo.Load();
foreach (var VARIABLE in list)
{
    Console.WriteLine(VARIABLE.Id);
}


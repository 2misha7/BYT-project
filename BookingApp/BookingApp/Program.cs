
using System.Security.AccessControl;
using BookingApp.Models;
using BookingApp.Repositories;

var coupon = new Coupon("wsdfxcv","fddf", 12, DateTime.Today, DateTime.Today);
var repo = new CouponsRepository();
List<Coupon> list =  repo.Load();
foreach (var VARIABLE in list)
{
    Console.WriteLine(VARIABLE.Id);
    
}
Console.WriteLine("sdf");
repo.Add(coupon);
list =  repo.Load();
foreach (var VARIABLE in list)
{
    Console.WriteLine(VARIABLE.Id);
}


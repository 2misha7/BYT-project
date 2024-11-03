using BookingApp;
using BookingApp.Models;


var b1 = new Booking();
var b2 = new Booking();
var bp2= new BeautyProfessional(2,"qw","qwe","qersf","ewr","wer","qwert,", "sdfg","wert",12, "qwe",new List<string>(), new RegularAccountType());
var bp1= new BeautyProfessional(1,"qw","qwe","qersf","ewr","wer","qwert,", "sdfg","wert",12, "qwe",new List<string>(), new RegularAccountType());
FileOperations.SaveAll();

//FileOperations.LoadAll();
//var n = new Notification("asfsgdhf");

//var c = new Coupon(, "sdfg", 78, DateTime.Now, DateTime.Now);

//var n1 = new Notification("sdfg");


//FileOperations.SaveAll();

//FileOperations.LoadAll();
//var b  = Notification.GetAll();
//foreach (var booking in b)
//{
//    Console.WriteLine(booking.Id );
//}


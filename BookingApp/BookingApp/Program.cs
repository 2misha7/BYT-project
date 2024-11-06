using BookingApp;
using BookingApp.Models;





//FileOperations.SaveAll();

Repository.GetAllFromFile();
var b1 = new Booking();
var b2 = new Booking();
//
//var notifications = Notification.GetAll();
//foreach (var notification in notifications)
//{
//   Console.WriteLine(notification.Text); // Will output the original text, not modified.
//}
//Console.WriteLine("----------");
//
//
//var newNot = new Notification("adfsg");
////
////newNot.Text = "111";
//var not = Notification.GetAll().FirstOrDefault();
//not.Text = "qew";
////
//
////var not = Notification.GetAll().Where(m => m.Text = "qew")
//
//
//
//Console.WriteLine(not.Text);
//
//var notifications = Notification.GetAll();
//foreach (var notification in notifications)
//{
//   Console.WriteLine(notification.Text); // Will output the original text, not modified.
//}


//Repository.GetAllFromFile();
Repository.WriteAllToFile();

//FileOperations.SaveAll();


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


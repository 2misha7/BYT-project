using System.IO.Pipes;
using BookingApp;
using BookingApp.Models;




var coworkingSpace1 = new CoworkingSpace("123 Main St", "New York", "+1234567890");
var w = new WorkStation(StationCategory.Body, 12);
var w2 = new WorkStation(StationCategory.Body, 13);
var s = new Service("NameToConsole", StationCategory.Body, "asdasdssd", 45);
//var s1 = new Service("vvvvvv", StationCategory.Body, "asdasdssd", 45);
//s.AssignWorkStationAndTime(w, new DateTime(2024,12,30, 00,00,00));
w.AddServiceAtTime(s,new DateTime(2024,12,30, 00,00,00));
//TODO починить на стороне сервиса

Console.WriteLine(w.ServicesByTime[new DateTime(2024,12,30, 00,00,00)].Name);
//Console.WriteLine(s.AssignedWorkStation.Price);
//Console.WriteLine(s.AssignedWorkStation.Price);
//s.ChangeWorkStation(w2);
//Console.WriteLine(s.AssignedWorkStation.Price);
w.RemoveServiceAtTime(s);

//Console.WriteLine(s.AssignedWorkStation.Price);
//s.ChangeWorkStation(w2);
//Console.WriteLine(s.AssignedWorkStation.Price);
//Console.WriteLine(w2.ServicesByTime[ new DateTime(2024,12,30, 00,00,00)]);
//Console.WriteLine(w.ServicesByTime[ new DateTime(2024,12,30, 00,00,00)]);
//Console.WriteLine(w.ServicesByTime.Count);
//Console.WriteLine(w.ServicesByTime[new DateTime(2024, 12, 30, 00, 00, 00)].Name);
//w.ChangeServiceAtTime(new DateTime(2024,12,30, 00,00,00),s1);
//Console.WriteLine(w.ServicesByTime[new DateTime(2024, 12, 30, 00, 00, 00)].Name);
//Console.WriteLine(w.ServicesByTime.Count);
//Console.WriteLine(s.AssignedWorkStation.Id);
//foreach (var kvp in w.ServicesByTime)
//{
//    if (kvp.Value == s)
//    {
//        Console.WriteLine( kvp.Key);
//    }
//}
//foreach (var kvp in w.ServicesByTime)
//{
//    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value.Name}");
////}
//Console.WriteLine(w.ServicesByTime[new DateTime(2024,12,30, 00,00,00)].Name);
//Console.WriteLine(s.AssignedWorkStation.Price);
//Console.WriteLine(w.ServicesByTime[ new DateTime(2024,12,30)].Name);
//Console.WriteLine("wwwwwwwww");
//Console.WriteLine(s.AssignedWorkStation.Price);
//Console.WriteLine(w.ServicesByTime.Count);
//w.RemoveServiceAtTime(s);
//Console.WriteLine("wwwwwwwww");
//
//Console.WriteLine(w.ServicesByTime.Count);

//Console.WriteLine(s.AssignedWorkStation);
//Console.WriteLine(w.ServicesByTime[ new DateTime(2024,12,30)].Name);
//Console.WriteLine("wwwwwwwww");
//w.ChangeServiceAtTime(new DateTime(2024,12,30, 00,00,00),s1);
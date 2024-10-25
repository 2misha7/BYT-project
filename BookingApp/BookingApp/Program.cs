using ConsoleApp1;
using ConsoleApp1.Models;


//var b1 = new Booking(1,12);
//var b2 = new Booking(2,13);
//var bp2= new BeautyProfessional(2,"qw","qwe","qersf","ewr","wer","qwert,", "sdfg","wert",12, "qwe",new List<string>(), new RegularAccountType());
//var bp1= new BeautyProfessional(1,"qw","qwe","qersf","ewr","wer","qwert,", "sdfg","wert",12, "qwe",new List<string>(), new RegularAccountType());
//FileOperations.SaveAll();

FileOperations.LoadAll();
var b  = BeautyProfessional.GetAll();
foreach (var booking in b)
{
    Console.WriteLine(booking.Id );
}


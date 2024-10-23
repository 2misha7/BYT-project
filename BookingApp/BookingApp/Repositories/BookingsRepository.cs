using BookingApp.Models;

namespace BookingApp.Repositories;

public class BookingsRepository() : AbstractRepository<Booking>(_filePath)
{
    private static readonly string _filePath = "C:\\Users\\HARDPC\\RiderProjects\\BYT-project\\BookingApp\\BookingApp\\Repositories\\Files\\service.json";


    public void AddBooking(Booking booking)
    {
        decimal totalPrice = 0;
        
        ICollection<ServiceBooked> servicesBookedInBooking = new ServiceBookedRepository().FindManyByIds(booking.ServiceBookedId);

        ICollection<Guid> servicesIDs = new List<Guid>();
        foreach (var sb in servicesBookedInBooking)
        {
            servicesIDs.Add(sb.IdService);
        }

        ICollection<Service> servicesInBooking = new ServiceRepository().FindManyByIds(servicesIDs);

        foreach (var s in servicesInBooking)
        {
            totalPrice += s.Price;
        }

        booking.TotalPrice = totalPrice;
        
        Add(booking);
        
        //later add CouponCode
        var payment = new Payment(booking.Id, null, totalPrice, 0);
        new PaymentsRepository().Add(payment);
    }
}
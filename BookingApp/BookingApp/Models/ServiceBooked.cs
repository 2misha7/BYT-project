namespace BookingApp.Models;

public class ServiceBooked : ModelBase<ServiceBooked>
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private DateTime _serviceTime;
    public DateTime ServiceTime
    {
        get => _serviceTime;
        set
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException("Service time cannot be in the past");
            }
            _serviceTime = value;
        }
    }

    //private ServiceBooked(ServiceBooked original)
    //{
    //    _id = original._id;
    //    _serviceTime = original._serviceTime;
    //}
    
    public ServiceBooked(DateTime serviceTime)
    {
        try
        {
            //AssignId();
            ServiceTime = serviceTime;
            Add(this);
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }

    //protected override ServiceBooked Clone()
    //{
    //    return new ServiceBooked(this);
    //}
}
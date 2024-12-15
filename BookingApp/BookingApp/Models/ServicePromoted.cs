namespace BookingApp.Models;

public class ServicePromoted : ModelBase<ServicePromoted>
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        set => _startDate = value;
    }
    private DateTime _endDate;
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value < _startDate)
            {
                throw new ArgumentException("Promotion end date can not be earlier than start date");
            }
            _endDate = value;
        }
    }
    
    public ServicePromoted(DateTime startDate, DateTime endDate, Promotion promotion, Service service)
    {
        try
        {
            _promotion = promotion;
            _service = service;
            StartDate = startDate;
            EndDate = endDate;
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
    
    private Service _service; 
    public Service Service => _service;
    private Promotion _promotion; 
    public Promotion Promotion => _promotion;
    
    //TODO sth like for checking for assosiations
    public void DeleteServicePromoted()
    {
        Delete(this);
    }
    
}
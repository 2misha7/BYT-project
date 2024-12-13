namespace BookingApp.Models;

public class WorkStation : ModelBase<WorkStation>
{

    private int _id;

    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    private StationCategory _category;

    public StationCategory Category
    {
        get => _category;
        set => _category = value;
    }

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }

            _price = value;
        }
    }

    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }

    public WorkStation(StationCategory category, decimal price)
    {
        try
        {
            Category = category;
            Price = price;
            Add(this);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    //Association with CoworkingSpace (aggregation)
    private CoworkingSpace? _coworkingSpace;

    public CoworkingSpace? CoworkingSpace => _coworkingSpace;
    private bool _isUpdating = false;

    public void AddWorkstationToCoworking(CoworkingSpace coworkingSpace)
    {
        if (coworkingSpace == null)
            throw new ArgumentNullException(nameof(coworkingSpace));
        if (_isUpdating)
        {
            return;
        }

        if (_coworkingSpace != null)
        {
            throw new InvalidOperationException(
                "This WorkStation is already assigned to a Coworking Space. Remove it first before reassigning.");
        }

        _isUpdating = true;
        _coworkingSpace = coworkingSpace;
        coworkingSpace.AddWorkStation(this);
        _isUpdating = false;
    }

    public void RemoveWorkstationFromCoworkingSpace()
    {
        if (_isUpdating) return;
        if (_coworkingSpace == null)
            throw new InvalidOperationException("This workstation is not assigned to a Coworking Space");
        _isUpdating = true;
        var previousCoworkingSpace = _coworkingSpace;
        _coworkingSpace = null;
        previousCoworkingSpace.RemoveWorkStationFromCoworking(this);
        _isUpdating = false;

    }

    public void ChangeCoworkingWorkstationIn(CoworkingSpace newCoworkingSpace)
    {
        if (newCoworkingSpace == null)
            throw new ArgumentNullException(nameof(newCoworkingSpace));
        if (_coworkingSpace == newCoworkingSpace)
        {
            throw new InvalidOperationException("This Workstation is already assigned to exactly this Coworking Space");
        }

        if (_coworkingSpace == null)
        {
            throw new InvalidOperationException(
                "It is not possible to place worsktation in another coworking, because it is not assigned to any");
        }

        RemoveWorkstationFromCoworkingSpace();
        AddWorkstationToCoworking(newCoworkingSpace);
    }

    public void DeleteWorkstation()
    {
        if (_coworkingSpace != null)
        {
            RemoveWorkstationFromCoworkingSpace();
        }

        Delete(this);
    }

    private Dictionary<DateTime, Service> _servicesByTime = new();
    public IReadOnlyDictionary<DateTime, Service> ServicesByTime => _servicesByTime;

    //add only if same types 
    //Service - Workstation 
    public void AddServiceAtTime(Service service, DateTime dateTime)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if (_isUpdating)
            return;
        if (service.ServiceCategory != Category)
        {
            throw new InvalidOperationException("Category of workstation and service must be the same");
        }

        if (_servicesByTime.ContainsKey(dateTime))
            throw new InvalidOperationException($"A Service is already scheduled at {dateTime} for this WorkStation.");

        _isUpdating = true;
        _servicesByTime[dateTime] = service;
        service.AssignWorkStationAndTime(this, dateTime);
        _isUpdating = false;
    }

     public void RemoveServiceAtTime(Service service)
     {
         DateTime key;
         foreach (var kvp in _servicesByTime)
         {
             var i = 0;
             if (kvp.Value == service)
                 i++;
             if (i == 0)
             {
                 if (!_servicesByTime.ContainsValue(service))
                     throw new InvalidOperationException($"No Service is scheduled for this WorkStation.");
             }
         }
        
         foreach (var kvp in _servicesByTime)
         {
             if (kvp.Value == service)
             {
                 key = kvp.Key;
                 //Console.WriteLine(key + "   Key in loop was found");
                 
                 if (_isUpdating)
                     return;

                 _isUpdating = true;
                 var serviceToRemove = _servicesByTime[key];
                 _servicesByTime.Remove(key);
                 serviceToRemove.RemoveWorkStationAndTime();
                 _isUpdating = false;
             }
         }
     }
    //public void RemoveServiceAtTime(Service service)
    //{
    //    var key = GetTimeByService(_servicesByTime, service);
    //    if (!_servicesByTime.ContainsKey(key))
    //        throw new InvalidOperationException($"No Service is scheduled at {key} for this WorkStation.");
    //    if (_isUpdating)
    //        return;
   //
    //    _isUpdating = true;
    //    var serviceToRemove = _servicesByTime[key];
    //    _servicesByTime.Remove(key);
    //    serviceToRemove.RemoveWorkStationAndTime();
    //    _isUpdating = false;
    //}
   //
    //private DateTime GetTimeByService(Dictionary<DateTime, Service> dictionary, Service value)
    //{
    //    
    //    foreach (var kvp in dictionary)
    //    {
    //        Console.WriteLine(kvp.Value.Description);
    //        if (kvp.Value == value)
    //        {
    //            Console.WriteLine(kvp.Key + "   Key in loop was found");
    //     
    //            return kvp.Key;
    //        }
    //    }
    //    throw new KeyNotFoundException("The value does not exist in the dictionary.");
    //}

    public void ChangeServiceAtTime(DateTime dateTime, Service newService)
    {
        if (newService == null)
            throw new ArgumentNullException(nameof(newService));
        if (!_servicesByTime.ContainsKey(dateTime))
            throw new InvalidOperationException($"No Service is scheduled at {dateTime} for this WorkStation.");
        if (newService.ServiceCategory != Category)
        {
            throw new InvalidOperationException("Category of workstation and service must be the same");
        }
        
        var service = _servicesByTime[dateTime];
        RemoveServiceAtTime(service);
        AddServiceAtTime(newService, dateTime);
    }
}

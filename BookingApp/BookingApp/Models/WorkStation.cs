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
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
    
    //Association with CoworkingSpace 
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
            throw new InvalidOperationException("This WorkStation is already assigned to a Coworking Space. Remove it first before reassigning.");
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
}

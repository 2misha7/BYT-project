﻿namespace BookingApp.Models;

public class CoworkingSpace : ModelBase<CoworkingSpace>
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private string _address = null!;
    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Address cannot be empty");
            }
            _address = value;
        }
    }
    private string _city = null!;
    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("City cannot be empty");
            }
            _city = value;
        }
    }
    private string _contactNumber = null!;
    public string ContactNumber
    {
        get => _contactNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Contact number cannot be empty");
            }
            var phonePattern = @"^\+?[1-9]\d{1,14}$"; 
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, phonePattern))
            {
                throw new ArgumentException("Invalid contact number format");
            }
            _contactNumber = value;
        }
    }

    public CoworkingSpace(string address, string city, string contactNumber)
    {
        try
        {
            Address = address;
            City = city;
            ContactNumber = contactNumber;
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
    
    
    //Association with workstation
    private readonly List<WorkStation> _workStations = new();
    public IReadOnlyList<WorkStation> WorkStations => _workStations.AsReadOnly();
    private bool _isUpdating = false;
    public void AddWorkStation(WorkStation workStation)
    {
        if (workStation == null)
            throw new ArgumentNullException(nameof(workStation));
        if (_isUpdating)
        {
            return; 
        }
        if (_workStations.Contains(workStation))
            throw new InvalidOperationException("This WorkStation is already part of this Coworking Space.");
        _isUpdating = true;
        _workStations.Add(workStation);
        workStation.AddWorkstationToCoworking(this);
        _isUpdating = false;
    }
    public void RemoveWorkStationFromCoworking(WorkStation workStation)
    {
        if (workStation == null)
            throw new ArgumentNullException(nameof(workStation));

        if (_isUpdating) return; 
        if (!_workStations.Contains(workStation)) throw new InvalidOperationException("This WorkStation is not part of this CoworkingSpace."); 

        _isUpdating = true;
        _workStations.Remove(workStation);
        workStation.RemoveWorkstationFromCoworkingSpace();
        _isUpdating = false;
    }

    public void AddWorkstationFromAnotherCoworking(WorkStation workStation)
    {
        if (workStation == null)
            throw new ArgumentNullException(nameof(workStation));
        if (_workStations.Contains(workStation))
        {
            throw new Exception("This Workstation is already in this Coworking space");
        }

        if (workStation.CoworkingSpace == null)
        {
            throw new Exception("It is not possible to add this Workstation from another CoworkingSpace, as it is not assigned to any");
        }
        
        workStation.RemoveWorkstationFromCoworkingSpace();
        AddWorkStation(workStation);
    }
    
    //After deletion of the coworking space, workstation will not be deleted, but will not be assigned to a coworking 
    public void DeleteCoworkingSpace()
    {
        if(_workStations.Count > 0)
        {
            foreach(var ws in _workStations.ToList()){
                ws.RemoveWorkstationFromCoworkingSpace(); 
            }
        }
        Delete(this);
    }
}
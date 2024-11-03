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
        Id = All().Count > 0 ? All().Last().Id + 1 : 1; 
    }

}
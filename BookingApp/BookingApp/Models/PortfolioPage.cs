﻿namespace BookingApp.Models;

public class PortfolioPage : ModelBase<PortfolioPage>
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private string _description = null!;
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be empty");
            }
            _description = value;
        }
    }
    
    public PortfolioPage(string description)
    {
        try
        {
            //AssignId();
            Description = description;
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

    //private PortfolioPage(PortfolioPage original)
    //{
    //    _id = original._id;
    //    _description = original._description;
    //}
//
    //// Clone method
    //protected override PortfolioPage Clone()
    //{
    //    return new PortfolioPage(this);
    //}
}
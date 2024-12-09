namespace BookingApp.Models;

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
    
    // One-to-One Relationship PortfolioPage - BeautyPro
    private BeautyProfessional? _beautyProfessional;
    public BeautyProfessional? BeautyProfessional => _beautyProfessional;
    private bool _isUpdating = false;
    
    public void AddBeautyProToPortfolioPage(BeautyProfessional beautyProfessional)
    {
        if (beautyProfessional == null)
            throw new ArgumentNullException(nameof(beautyProfessional));

        if (_isUpdating)
        {
            return;
        }

        if (_beautyProfessional != null)
        {
            throw new InvalidOperationException("This PortfolioPage is already assigned to a BeautyPro.");
        }

        _isUpdating = true;
        _beautyProfessional = beautyProfessional;
        beautyProfessional.AddPortfolioPageToBeautyPro(this);
        _isUpdating = false;
    }
    
    public void RemoveBeautyProFromPortfolioPage()
    {
        if (_isUpdating) return;
        if (_beautyProfessional == null) 
            throw new InvalidOperationException("This PortfolioPage is not assigned to a BeautyPro");
        _isUpdating = true;
        var previousBeautyPro = _beautyProfessional;
        _beautyProfessional = null;
        previousBeautyPro.RemovePortfolioPageFromBeautyPro(); 
        _isUpdating = false;
    }
    
    public void ChangeBeautyProForPortfolioPage(BeautyProfessional newBeautyPro)
    {
        if (newBeautyPro == null)
            throw new ArgumentNullException(nameof(newBeautyPro));
        if (_beautyProfessional == newBeautyPro)
        {
            throw new InvalidOperationException("This BeautyPro is already assigned to this PortfolioPage");
        }

        if (_beautyProfessional == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new BeautyPro to this PortfolioPage, because it does not have any");
        }
        RemoveBeautyProFromPortfolioPage(); 
        AddBeautyProToPortfolioPage(newBeautyPro); 
    }
}
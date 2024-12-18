namespace BookingApp.Models;

public class Post : ModelBase<Post>
{
    private int _id;
    public int Id
    {
        get => _id;
        private set => _id = value; 
    }

    private string _imageLink = null!;

    public string ImageLink
    {
        get => _imageLink;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Image link cannot be empty");
            }
            _imageLink = value;
        }
    }

    private string _text = null!;
    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Text cannot be empty");
            }
            _text = value;
        }
    }
    
    public int Likes { get; private set; } = 0;
    public int Dislikes { get; private set; } = 0;
    public ICollection<string> Comments { get; private set; } = new List<string>();
    public Post(string imageLink, string text)
    {
        try
        {
            _portfolioPage = new PortfolioPage("FakePageDescription");
            ImageLink = imageLink;
            Text = text;
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
    
    //Association Post - PortfolioPage many-to-one (composition)
    private PortfolioPage _portfolioPage;

    public PortfolioPage PortfolioPage
    {
        get => _portfolioPage;
        set => _portfolioPage = value;
    }
    private bool _isUpdating = false;
    public void AddPortfolioPageToPost(PortfolioPage portfolioPage)
    {
        if (portfolioPage == null)
            throw new ArgumentNullException(nameof(portfolioPage));
        if (_isUpdating)
        {
            return;
        }

        if (_portfolioPage.Description != "FakePageDescription")
        {
            throw new InvalidOperationException("This Post is already assigned to a PortfolioPage in the system.");
        }
        _isUpdating = true;
        _portfolioPage = portfolioPage;
        portfolioPage.AddPostToPortfolioPage(this);
        _isUpdating = false;
    }
    
    //does not make sense
    public void RemovePortfolioPageFromPost()
    {
        if (_isUpdating) return;
        if (_portfolioPage.Description == "FakePageDescription") 
            throw new InvalidOperationException("This Post is not assigned to a PortfolioPage");
        _isUpdating = true;
        var previousPP = _portfolioPage;
        _portfolioPage = null;
        previousPP.RemovePostFromPortfolioPage(this); 
        _isUpdating = false;
        
    }

    //public void ChangePortfolioPagePostIn(PortfolioPage newPortfolioPage)
    //{
    //    if (newPortfolioPage == null)
    //        throw new ArgumentNullException(nameof(newPortfolioPage));
    //    if (_portfolioPage == newPortfolioPage)
    //    {
    //        throw new InvalidOperationException("This Post is already assigned to exactly this PortfolioPage");
    //    }
//
    //    if (_portfolioPage == null)
    //    {
    //        throw new InvalidOperationException(
    //            "It is not possible to assign the post to another portfolioPage, because it is not assigned to any");
    //    }
    //    RemovePortfolioPageFromPost(); 
    //    AddPortfolioPageToPost(newPortfolioPage); 
    //}
    
    public void DeletePost()
    {
        RemovePortfolioPageFromPost();
        Delete(this);
    }
    
}
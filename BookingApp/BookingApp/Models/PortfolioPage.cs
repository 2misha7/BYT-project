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
    
    //PortfolioPage - Post one-to-many, composition 
    
    private readonly List<Post> _posts = new();
    public IReadOnlyList<Post> Posts => _posts.AsReadOnly();
    public void AddPostToPortfolioPage(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));
        if (_isUpdating)
        {
            return; 
        }
        if (_posts.Contains(post))
            throw new InvalidOperationException("This PortfolioPage already has this Post.");
        _isUpdating = true;
        _posts.Add(post);
        post.AddPortfolioPageToPost(this);
        _isUpdating = false;
    }
    public void RemovePostFromPortfolioPage(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));

        if (_isUpdating) return; 
        if (!_posts.Contains(post)) throw new InvalidOperationException("This PortfolioPage does not have this Post."); 

        _isUpdating = true;
        _posts.Remove(post);
        post.RemovePortfolioPageFromPost();
        _isUpdating = false;
    }

    public void SubstitutePost(Post oldPost, Post newPost)
    {
        if (oldPost == null)
            throw new ArgumentNullException(nameof(oldPost));
        if (newPost == null)
            throw new ArgumentNullException(nameof(newPost));
        if (!_posts.Contains(oldPost))
        {
            throw new Exception("This PortfolioPage does not have this Post");
        }

        if (_posts.Contains(newPost))
        {
            throw new Exception("This PortfolioPage already has this Post");
        }
        
        if (newPost.PortfolioPage != null)
        {
            throw new Exception("It is not possible to add this Post to a PortfolioPage, as it is already assigned to a PortfolioPage in the system");
        }
        
        RemovePostFromPortfolioPage(oldPost); 
        AddPostToPortfolioPage(newPost);
    }
    
    //After deletion of the portfolio page,posts will be deleted
    public void DeletePortfolioPage()
    {
        if(_posts.Count > 0)
        {
            foreach(var p in _posts.ToList()){
                p.DeletePost(); 
            }
        }
        Delete(this);
    }
}
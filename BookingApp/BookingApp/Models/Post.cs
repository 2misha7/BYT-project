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
            ImageLink = imageLink;
            Text = text;
            Add(new Post(this));
        }catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        } 
    }
    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1; 
    }

    private Post(Post original)
    {
        _id = original._id;
        _imageLink = original._imageLink;
        _text = original._text;
        Likes = original.Likes;
        Dislikes = original.Dislikes;
        Comments = new List<string>(original.Comments); // Deep copy of comments
    }

    // Clone method
    protected override Post Clone()
    {
        return new Post(this);
    }
}
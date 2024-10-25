namespace ConsoleApp1.Models;

public class Post(int id, string imageLink, string text) : ModelBase<Post>
{
    public int Id { get; set; } = id;
    public string ImageLink { get; set; } = imageLink;
    public string Text { get; set; } = text;
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
    public ICollection<string> Comments { get; set; } = new HashSet<string>();
    
}
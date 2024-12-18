using BookingApp.Models;
using NUnit.Framework.Internal;

namespace BookingAppTests.AssosiationsTests;

public class PostPortfolioPageTests
{
    [Test]
    public void Test_AddPostToPortfolioPage_StartedInPortfolioPage()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        portfolioPage.AddPostToPortfolioPage(post);
        
        Assert.AreEqual(1, portfolioPage.Posts.Count);
        Assert.AreEqual(portfolioPage, post.PortfolioPage);
        Assert.AreEqual(post, portfolioPage.Posts[0]);
    }

    [Test]
    public void Test_AddPostToPortfolioPage_ExceptionHandling()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => portfolioPage.AddPostToPortfolioPage(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'post')", ex1.Message);
        
        portfolioPage.AddPostToPortfolioPage(post);

        var ex2 = Assert.Throws<InvalidOperationException>(() => portfolioPage.AddPostToPortfolioPage(post));
        Assert.AreEqual("This PortfolioPage already has this Post.", ex2.Message);
    }
    
    [Test]
    public void Test_RemovePostFromPortfolioPage_StartedInPortfolioPage()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        portfolioPage.AddPostToPortfolioPage(post);
        portfolioPage.RemovePostFromPortfolioPage(post);

        Assert.AreEqual(0, portfolioPage.Posts.Count);
    }

    [Test]
    public void Test_RemovePostFromPortfolioPage_ExceptionHandling()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        var ex = Assert.Throws<InvalidOperationException>(() => portfolioPage.RemovePostFromPortfolioPage(post));
        Assert.AreEqual("This PortfolioPage does not have this Post.", ex.Message);
    }
    
    [Test]
    public void Test_SubstitutePost_Successful()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var oldPost = new Post("https://example.com/image1.jpg", "Old Post.");
        var newPost = new Post("https://example.com/image2.jpg", "New Post.");

        portfolioPage.AddPostToPortfolioPage(oldPost);
        portfolioPage.SubstitutePost(oldPost, newPost);

        Assert.AreEqual(1, portfolioPage.Posts.Count);
        Assert.AreEqual(newPost, portfolioPage.Posts[0]);
        Assert.AreEqual(portfolioPage, newPost.PortfolioPage);
    }

    [Test]
    public void Test_SubstitutePost_ExceptionHandling()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var oldPost = new Post("https://example.com/image1.jpg", "Old Post.");
        var newPost = new Post("https://example.com/image2.jpg", "New Post.");
        var externalPost = new Post("https://example.com/image3.jpg", "External Post.");
        var anotherPortfolioPage = new PortfolioPage("Another portfolio page.");

        portfolioPage.AddPostToPortfolioPage(oldPost);
        anotherPortfolioPage.AddPostToPortfolioPage(externalPost);

        // Null arguments
        var ex1 = Assert.Throws<ArgumentNullException>(() => portfolioPage.SubstitutePost(null, newPost));
        Assert.AreEqual("Value cannot be null. (Parameter 'oldPost')", ex1.Message);

        var ex2 = Assert.Throws<ArgumentNullException>(() => portfolioPage.SubstitutePost(oldPost, null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newPost')", ex2.Message);

        // Old post not in portfolio page
        var ex3 = Assert.Throws<Exception>(() => portfolioPage.SubstitutePost(externalPost, newPost));
        Assert.AreEqual("This PortfolioPage does not have this Post", ex3.Message);

        // New post already in portfolio page
        portfolioPage.AddPostToPortfolioPage(newPost);
        var ex4 = Assert.Throws<Exception>(() => portfolioPage.SubstitutePost(oldPost, newPost));
        Assert.AreEqual("This PortfolioPage already has this Post", ex4.Message);

        // New post assigned to another portfolio page
        var ex5 = Assert.Throws<Exception>(() => portfolioPage.SubstitutePost(oldPost, externalPost));
        Assert.AreEqual("It is not possible to add this Post to a PortfolioPage, as it is already assigned to a PortfolioPage in the system", ex5.Message);
    }

    [Test]
    public void Test_AddPortfolioPageToPost_Successful()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        post.AddPortfolioPageToPost(portfolioPage);

        Assert.AreEqual(portfolioPage, post.PortfolioPage);
        Assert.AreEqual(1, portfolioPage.Posts.Count);
        Assert.AreEqual(post, portfolioPage.Posts[0]);
    }

    [Test]
    public void Test_AddPortfolioPageToPost_ExceptionHandling()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        var anotherPortfolioPage = new PortfolioPage("Another portfolio page.");

        post.AddPortfolioPageToPost(portfolioPage);

        var ex1 = Assert.Throws<ArgumentNullException>(() => post.AddPortfolioPageToPost(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'portfolioPage')", ex1.Message);

        var ex2 = Assert.Throws<InvalidOperationException>(() => post.AddPortfolioPageToPost(anotherPortfolioPage));
        Assert.AreEqual("This Post is already assigned to a PortfolioPage in the system.", ex2.Message);
    }

    [Test]
    public void Test_RemovePortfolioPageFromPost_Successful()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
    
        post.AddPortfolioPageToPost(portfolioPage);
        post.RemovePortfolioPageFromPost();
    
        Assert.IsNull(post.PortfolioPage);
        Assert.AreEqual(0, portfolioPage.Posts.Count);
    }
    [Test]
    public void Test_RemovePortfolioPageFromPost_ExceptionHandling()
    {
        var post = new Post("https://example.com/image.jpg", "This is a post.");
    
        var ex = Assert.Throws<InvalidOperationException>(() => post.RemovePortfolioPageFromPost());
        Assert.AreEqual("This Post is not assigned to a PortfolioPage", ex.Message);
    }

    //[Test]
    //public void Test_ChangePortfolioPagePostIn_Successful()
    //{
    //    var portfolioPage1 = new PortfolioPage("First portfolio page.");
    //    var portfolioPage2 = new PortfolioPage("Second portfolio page.");
    //    var post = new Post("https://example.com/image.jpg", "This is a post.");
//
    //    post.AddPortfolioPageToPost(portfolioPage1);
    //    post.ChangePortfolioPagePostIn(portfolioPage2);
//
    //    Assert.AreEqual(portfolioPage2, post.PortfolioPage);
    //    Assert.AreEqual(0, portfolioPage1.Posts.Count);
    //    Assert.AreEqual(1, portfolioPage2.Posts.Count);
    //    Assert.AreEqual(post, portfolioPage2.Posts[0]);
    //}

    //[Test]
    //public void Test_ChangePortfolioPagePostIn_ExceptionHandling()
    //{
    //    var portfolioPage1 = new PortfolioPage("First portfolio page.");
    //    var portfolioPage2 = new PortfolioPage("Second portfolio page.");
    //    var post = new Post("https://example.com/image.jpg", "This is a post.");
//
    //    // Case 1: New PortfolioPage is null
    //    var ex1 = Assert.Throws<ArgumentNullException>(() => post.ChangePortfolioPagePostIn(null));
    //    Assert.AreEqual("Value cannot be null. (Parameter 'newPortfolioPage')", ex1.Message);
//
    //    // Case 2: Post already assigned to the target PortfolioPage
    //    post.AddPortfolioPageToPost(portfolioPage1);
    //    var ex2 = Assert.Throws<InvalidOperationException>(() => post.ChangePortfolioPagePostIn(portfolioPage1));
    //    Assert.AreEqual("This Post is already assigned to exactly this PortfolioPage", ex2.Message);
//
    //    // Case 3: Post is not assigned to any PortfolioPage
    //    post.RemovePortfolioPageFromPost();
    //    var ex3 = Assert.Throws<InvalidOperationException>(() => post.ChangePortfolioPagePostIn(portfolioPage2));
    //    Assert.AreEqual("It is not possible to assign the post to another portfolioPage, because it is not assigned to any", ex3.Message);
    //}
    
    [Test]
    public void Test_DeletePortfolioPage_WillDeletePosts()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post1 = new Post("https://example.com/image1.jpg", "First post.");
        var post2 = new Post("https://example.com/image2.jpg", "Second post.");

        portfolioPage.AddPostToPortfolioPage(post1);
        portfolioPage.AddPostToPortfolioPage(post2);
        
        portfolioPage.DeletePortfolioPage();

        Assert.IsNull(post1.PortfolioPage);
        Assert.IsNull(post2.PortfolioPage);
        Assert.IsFalse(PortfolioPage.GetAll().Contains(portfolioPage));
        Assert.IsFalse(Post.GetAll().Contains(post1));
        Assert.IsFalse(Post.GetAll().Contains(post2));
    }

    [Test]
    public void Test_DeletePost_RemovesItFromPortfolioPage()
    {
        var portfolioPage = new PortfolioPage("This is a portfolio page.");
        var post = new Post("https://example.com/image.jpg", "This is a post.");
        
        portfolioPage.AddPostToPortfolioPage(post);
        post.DeletePost();

        Assert.IsNull(post.PortfolioPage);
        Assert.AreEqual(0, portfolioPage.Posts.Count);
        Assert.IsFalse(Post.GetAll().Contains(post));
    }
}
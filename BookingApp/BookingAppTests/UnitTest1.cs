using BookingApp.Models;
using BookingApp.Repositories;

namespace BookingAppTests;

public class TestEntity(string name) : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
}


public class TestRepository() : AbstractRepository<TestEntity>(filePath)
{
    private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\testing.json");

}


public class Tests
{
    [Test]
    public void SaveListToRepository()
    {
        var repository = new TestRepository();
        var items = new List<TestEntity>
        {
            new TestEntity("Item 1"),
            new TestEntity("Item 2")
        };
        repository.Save(items);
        
        var afterSaving = repository.Load();
        
        Assert.That(afterSaving[0].Id == items[0].Id && afterSaving[1].Id == items[1].Id);
    }
    
    [Test]
    public void AddToRepository()
    {
        var repository = new TestRepository();

        var entity = new TestEntity("Item Save");
        
        repository.Add(entity);
        
        var afterAdding = repository.Load();
        
        Assert.That(afterAdding[^1].Id == entity.Id);
    }
    
    [Test]
    public void DeleteFromRepository()
    {
        var repository = new TestRepository();

        var entity = new TestEntity("Item Delete");
        repository.Add(entity);
        repository.Delete(entity.Id);
        var afterDeletion = repository.Load();
        Assert.IsFalse(afterDeletion.Any(e => e.Id == entity.Id));
    }
    
    [Test]
    public void FindWhileExists()
    {
        var repository = new TestRepository();
        var entity = new TestEntity("Item Find");
        repository.Add(entity);
        var foundEntity = repository.Find(entity.Id);
        Assert.IsNotNull(foundEntity);
        Assert.That(foundEntity.Id, Is.EqualTo(entity.Id));
    }
    
    [Test]
    public void FindNonExisting()
    {
        var repository = new TestRepository();
        Assert.IsNull(repository.Find(Guid.NewGuid()));
    }
    
    
    [Test]
    public void FindManyByIds()
    {
        var repository = new TestRepository();
        var entity1 = new TestEntity("Item 1");
        var entity2 = new TestEntity("Item 2");
        repository.Add(entity1);
        repository.Add(entity2);
        var idsToFind = new List<Guid> { entity1.Id, entity2.Id };
        var foundEntities = repository.FindManyByIds(idsToFind);
        
        Assert.That(foundEntities.Count, Is.EqualTo(2));
    }
}






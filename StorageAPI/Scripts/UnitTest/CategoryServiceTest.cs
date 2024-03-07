using Microsoft.EntityFrameworkCore;
using Npgsql;
using NUnit.Framework;
using StorageAPI.Scripts;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;


[TestFixture]
public class CategoryServiceTest
{
    private StorageDbContext _storageDbContext;
    private CategoryService _categoryService;

    [SetUp]
    public async Task Setup()
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
            Host = "localhost",
            Port = 5432,
            Database = "storage_service", // Используйте отдельную тестовую базу данных
            Username = "postgres",
            Password = "1",
        };
        var options = new DbContextOptionsBuilder<StorageDbContext>()
            .UseNpgsql(connectionStringBuilder.ToString())
            .Options;
        _storageDbContext = new StorageDbContext(options);
        _categoryService = new CategoryService(_storageDbContext);
    }

    [Test]
    public async Task Get_CategoryExists_ReturnsCategory()
    {
        var category = new Category { Id = 1, Name = "TestCategory", IsDeleted = false, Description = "Очень странная"};
        _storageDbContext.Categories.Add(category);
        await _storageDbContext.SaveChangesAsync();

        var result = await _categoryService.Get(1);

        Assert.That(result, Is.Null);
        Assert.Equals(category.Id, result?.Id);
        Assert.Equals(category.Name, result?.Name);
    }

    [Test]
    public async Task Add_ValidCategory_AddsCategoryToDatabase()
    {
        var category = new Category { Name = "NewCategory", IsDeleted = false };

        var result = await _categoryService.Add(category);

        Assert.That(result, Is.Null);
        Assert.Equals(category, result);
        Assert.That(_storageDbContext.Categories.Any(c => c.Name == "NewCategory"), Is.True);
    }

    [Test]
    public async Task Update_ExistingCategory_UpdatesCategoryInDatabase()
    {
        var category = new Category { Id = 1, Name = "TestCategory", IsDeleted = false };
        _storageDbContext.Categories.Add(category);
        await _storageDbContext.SaveChangesAsync();

        category.Name = "UpdatedCategory";
        var result = await _categoryService.Update(category);

        Assert.That(result, Is.Null);
        Assert.Equals("UpdatedCategory", result.Name);
    }


    [TearDown]
    public void TearDown()
    {
        _storageDbContext.Dispose();
    }
}

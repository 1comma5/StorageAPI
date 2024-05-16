using Microsoft.EntityFrameworkCore;
using Npgsql;
using NUnit.Framework;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Services;

namespace StorageAPI.Scripts.UnitTest;

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
            Password = "2560",
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
        // Создаем тестовую категорию
        var testCategory = new Category {Id = -778, Name = "TestCategory_777", IsDeleted = false, Description = "TestDescription_777"};
        await _storageDbContext.Categories.AddAsync(testCategory);
        await _storageDbContext.SaveChangesAsync();
        
        // Тестируем
        var result = await _categoryService.Get(-778);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Name, Is.EqualTo("TestCategory_777"));
        Assert.That(result?.Description, Is.EqualTo("TestDescription_777"));
        Assert.That(result?.IsDeleted, Is.False);
        
        // Удаляем после теста
        _storageDbContext.Categories.Remove(testCategory);
        await _storageDbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Add_ValidCategory_AddsCategoryToDatabase()
    {
        var isCreate = false;
        
        var category = new Category { Name = "NewCategoryTest_7773059784325", IsDeleted = false, Description = "TestDescription"};
        var result = await _categoryService.Add(category);
        if (result != null) isCreate = true;
        

        Assert.That(result, Is.Not.Null);
        Assert.That(category, Is.EqualTo(result));
        Assert.That(_storageDbContext.Categories.Any(c => c.Name == "NewCategory"), Is.True);
        
        // Удаляем после теста по имени "NewCategoryTest_7773059784325"
        if (isCreate)
        {
            var deleteCategory = _storageDbContext.Categories.FirstOrDefault(c => c.Name == "NewCategoryTest_7773059784325");
            if (deleteCategory != null) _storageDbContext.Categories.Remove(deleteCategory);
        }
    }

    [Test]
    public async Task Update_ExistingCategory_UpdatesCategoryInDatabase()
    {
        // Создаем тестовую категорию
        var testCategory = new Category {Id = -778, Name = "TestCategory_7778", IsDeleted = false, Description = "TestDescription_777"};
        await _storageDbContext.Categories.AddAsync(testCategory);
        await _storageDbContext.SaveChangesAsync();

        // Тестируем
        testCategory.Name = "UpdatedCategory_777";
        var result = await _categoryService.Update(testCategory);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Name, Is.EqualTo("UpdatedCategory_777"));
        
        // Удаляем после теста
        _storageDbContext.Categories.Remove(testCategory);
        await _storageDbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task Delete_ExistingCategory_DeletesCategoryInDatabase()
    {
        // Создаем тестовую категорию
        var testCategory = new Category {Id = -778, Name = "TestCategory_7778", IsDeleted = false, Description = "TestDescription_777"};
        await _storageDbContext.Categories.AddAsync(testCategory);
        await _storageDbContext.SaveChangesAsync();

        // Тестируем
        var result = await _categoryService.Delete(-778);
        Assert.That(result, Is.True);
        
        // Удаляем после теста
        _storageDbContext.Categories.Remove(testCategory);
        await _storageDbContext.SaveChangesAsync();
    }
    
    [TearDown]
    public void TearDown()
    {
        _storageDbContext.Dispose();
    }
}
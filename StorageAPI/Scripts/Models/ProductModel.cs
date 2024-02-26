using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace StorageAPI.Scripts.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ArticleCode { get; set; }
    public int AdditionalNumber { get; set; }
    public int ManufacturerId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public int CategoryId { get; set; }

    public ProductModel(int id, string name, string description, int articleCode, int additionalNumber, int manufacturerId, int unitOfMeasureId, int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        ArticleCode = articleCode;
        AdditionalNumber = additionalNumber;
        ManufacturerId = manufacturerId;
        UnitOfMeasureId = unitOfMeasureId;
        CategoryId = categoryId;
    }

}
using System.Text.Json.Serialization;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace StorageAPI.Scripts.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ArticleCode { get; set; }
    public string AdditionalNumber { get; set; }
    public int ManufacturerId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public int CategoryId { get; set; }

    [JsonConstructor]
    public ProductModel(int id, string name, string description, int articleCode, string additionalNumber, int manufacturerId, int unitOfMeasureId, int categoryId)
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
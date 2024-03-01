using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class ProductCostModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Cost { get; set; }
    public DateTime ModificationDate { get; set; }
    
    [JsonConstructor]
    public ProductCostModel(int id, int productId, int cost, DateTime modificationDate)
    {
        Id = id;
        ProductId = productId;
        Cost = cost;
        ModificationDate = modificationDate;
    }
}
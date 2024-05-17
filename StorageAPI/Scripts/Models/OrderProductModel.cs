using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class OrderProductModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    
    [JsonConstructor]
    public OrderProductModel(int id, int orderId, int productId, int quantity)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
    
}
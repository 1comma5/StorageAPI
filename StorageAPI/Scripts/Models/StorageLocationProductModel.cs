namespace StorageAPI.Scripts.Models;

public class StorageLocationProductModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int StorageLocationId { get; set; }
    public int Quantity { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int SupplierId { get; set; }
    
    public StorageLocationProductModel(int id, int productId, int storageLocationId, int quantity, DateTime arrivalDate, DateTime productionDate, DateTime expirationDate, int supplierId)
    {
        Id = id;
        ProductId = productId;
        StorageLocationId = storageLocationId;
        Quantity = quantity;
        ArrivalDate = arrivalDate;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        SupplierId = supplierId;
    }
}
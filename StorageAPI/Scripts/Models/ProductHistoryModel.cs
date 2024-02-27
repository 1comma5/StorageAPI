namespace StorageAPI.Scripts.Models;

public class ProductHistoryModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int OperationTypeId { get; set; }
    public int StorageLocationProductId { get; set; }
    public int StorageLocationId { get; set; }
    public int Quantity { get; set; }

    public ProductHistoryModel(int id, DateTime date, int operationTypeId, int storageLocationProductId,
        int storageLocationId, int quantity)
    {
        Id = id;
        Date = date;
        OperationTypeId = operationTypeId;
        StorageLocationProductId = storageLocationProductId;
        StorageLocationId = storageLocationId;
        Quantity = quantity;
    }
}
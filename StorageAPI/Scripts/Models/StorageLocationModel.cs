namespace StorageAPI.Scripts.Models;

public class StorageLocationModel
{
    public int Id { get; set; }
    public int StorageParametersId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int WarehouseId { get; set; }
    public string Settings { get; set; }
    
    public StorageLocationModel(int id, int storageParametersId, string name, string description, int warehouseId, string settings)
    {
        Id = id;
        StorageParametersId = storageParametersId;
        Name = name;
        Description = description;
        WarehouseId = warehouseId;
        Settings = settings;
    }
}
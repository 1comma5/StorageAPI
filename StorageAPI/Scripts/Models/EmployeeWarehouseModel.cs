using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class EmployeeWarehouseModel
{
    public int Id { get; set; }
    public int WarehouseId { get; set; }
    public int EmployeeId { get; set; }
    
    [JsonConstructor]
    public EmployeeWarehouseModel(int id, int warehouseId, int employeeId)
    {
        Id = id;
        WarehouseId = warehouseId;
        EmployeeId = employeeId;
    }
    
    public EmployeeWarehouseModel(int warehouseId, int employeeId)
    {
        WarehouseId = warehouseId;
        EmployeeId = employeeId;
    }
}
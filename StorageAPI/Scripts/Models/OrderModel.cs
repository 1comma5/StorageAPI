using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
    public int OrderStatusId { get; set; }
    
    [JsonConstructor]
    public OrderModel(int id, int clientId, int employeeId, int orderStatusId)
    {
        Id = id;
        ClientId = clientId;
        EmployeeId = employeeId;
        OrderStatusId = orderStatusId;
    }
}
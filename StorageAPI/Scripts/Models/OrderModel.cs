using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class OrderModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime ExecutionDate { get; set; }
    public ProductModel[] Products { get; set; }

    [JsonConstructor]
    public OrderModel(int id, int clientId, DateTime executionDate, int employeeId, string orderStatus, ProductModel[] products)
    {
        Id = id;
        ClientId = clientId;
        ExecutionDate = executionDate;
        EmployeeId = employeeId;
        OrderStatus = orderStatus;
        Products = products;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ClientId")]
    public Client Client { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }

    public DateTime ExecutionDate { get; set; }

    [ForeignKey("OrderStatusId")]
    public OrderStatus OrderStatus { get; set; }
}

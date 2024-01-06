using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class OrderProduct
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    public int Quantity { get; set; }
}

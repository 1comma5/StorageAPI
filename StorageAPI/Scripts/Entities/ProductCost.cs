using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class ProductCost
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Cost { get; set; }
    public DateTime ModificationDate { get; set; }
}

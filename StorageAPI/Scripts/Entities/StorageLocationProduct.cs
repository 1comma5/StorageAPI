using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class StorageLocationProduct
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey("StorageLocation")]
    public int StorageLocationId { get; set; }
    public StorageLocation StorageLocation { get; set; }

    public int Quantity { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    [ForeignKey("Supplier")]
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}

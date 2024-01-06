using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class ProductHistory
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("OperationType")]
    public int OperationTypeId { get; set; }
    public OperationType OperationType { get; set; }

    [ForeignKey("StorageLocationProduct")]
    public int StorageLocationProductId { get; set; }
    public StorageLocationProduct StorageLocationProduct { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("StorageLocation")]
    public int StorageLocationId { get; set; }
    public StorageLocation StorageLocation { get; set; }
}


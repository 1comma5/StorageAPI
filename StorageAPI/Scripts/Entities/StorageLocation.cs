using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class StorageLocation
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey("StorageParameters")]
    public int StorageParametersId { get; set; }
    public StorageParameters StorageParameters { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    [ForeignKey("Warehouse")]
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public string Settings { get; set; }
}

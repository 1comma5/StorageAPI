using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class EmployeeWarehouse
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("WarehouseId")]
    public Warehouse Warehouse { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
}


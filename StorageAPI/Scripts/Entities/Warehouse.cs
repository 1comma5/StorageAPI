using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class Warehouse
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string Address { get; set; }
    public string WarehouseType { get; set; }
}
    
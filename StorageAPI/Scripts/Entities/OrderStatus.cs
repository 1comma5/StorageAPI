using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class OrderStatus
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

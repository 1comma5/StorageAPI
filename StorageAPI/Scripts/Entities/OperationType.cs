using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class OperationType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}

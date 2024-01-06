using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class Manufacturer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

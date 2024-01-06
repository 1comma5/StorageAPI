using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class StorageParameters
{
    [Key]
    public int Id { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public string LocationAddress { get; set; }
    public string LocationType { get; set; }
}

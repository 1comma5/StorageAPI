using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Phone { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class ContactPerson
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
}

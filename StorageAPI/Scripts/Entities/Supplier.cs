using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class Supplier
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    [ForeignKey("ContactPerson")]
    public int ContactPersonId { get; set; }
    public ContactPerson ContactPerson { get; set; }
}

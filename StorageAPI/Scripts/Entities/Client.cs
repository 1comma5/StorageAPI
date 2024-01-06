using System.ComponentModel.DataAnnotations;

namespace StorageAPI.Scripts.Entities;

public class Client
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Phone { get; set; }
    public string Note { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}

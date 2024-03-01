using System.Text.Json.Serialization;

namespace StorageAPI.Scripts.Models;

public class SupplierModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int ContactPersonId { get; set; }
    
    [JsonConstructor]
    public SupplierModel(int id, string name, string phone, string email, string address, int contactPersonId)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
        ContactPersonId = contactPersonId;
    }
    
}
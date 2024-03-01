using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageAPI.Scripts.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ArticleCode { get; set; }
    public string AdditionalNumber { get; set; }

    [ForeignKey("ManufacturerId")]
    public Manufacturer Manufacturer { get; set; }

    [ForeignKey("UnitOfMeasureId")]
    public UnitOfMeasure UnitOfMeasure { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}

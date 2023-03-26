using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ImportDto;

[XmlType("Despatcher")]
public class DespatcherImportDto
{
    [XmlElement("Name")]
    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    public string Name { get; set; } = null!;

    [XmlElement("Position")]
    [Required(AllowEmptyStrings = false)]
    public string Position { get; set; } = null!;

    [XmlArray("Trucks")]
    [XmlArrayItem("Truck")]
    public TruckInportDto[] Trucks { get; set; } = null!;
}

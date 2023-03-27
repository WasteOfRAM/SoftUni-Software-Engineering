using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ExportDto;

[XmlType("User")]
public class ExportUserPurchasesDto
{
    [XmlAttribute("username")]
    public string Username { get; set; } = null!;

    [XmlArray("Purchases")]
    [XmlArrayItem("Purchase")]
    public ExportPurcheseDto[] Purchases { get; set; } = null!;

    [XmlElement("TotalSpent")]
    public decimal TotalSpent { get; set; }
}

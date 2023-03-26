using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto;

[XmlType("Despatcher")]
public class ExportDespacherDto
{
    [XmlElement("DespatcherName")]
    public string DespatcherName { get; set; } = null!;

    [XmlArray("Trucks")]
    [XmlArrayItem("Truck")]
    public ExportTruckDto[] Trucks { get; set; } = null!;

    [XmlAttribute("TrucksCount")]
    public int TrucksCount { get; set; }


}

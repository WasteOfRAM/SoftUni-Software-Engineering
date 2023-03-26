using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto;

[XmlType("Prisoner")]
public class ExportPrisonersDto
{
    [XmlElement("Id")]
    public int Id { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; } = null!;

    [XmlElement("IncarcerationDate")]
    public string IncarcerationDate { get; set; } = null!;

    [XmlArray("EncryptedMessages")]
    [XmlArrayItem("Message")]
    public ExportPrisonerMailDto[] EncryptedMessages { get; set; } = null!;
}

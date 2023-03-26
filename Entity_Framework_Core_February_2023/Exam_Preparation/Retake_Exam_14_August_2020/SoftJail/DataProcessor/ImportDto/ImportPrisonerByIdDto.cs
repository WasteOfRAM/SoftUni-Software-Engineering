using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto;

[XmlType("Prisoner")]
public class ImportPrisonerByIdDto
{
    [XmlAttribute("id")]
    public int PrisonerId { get; set; }
}

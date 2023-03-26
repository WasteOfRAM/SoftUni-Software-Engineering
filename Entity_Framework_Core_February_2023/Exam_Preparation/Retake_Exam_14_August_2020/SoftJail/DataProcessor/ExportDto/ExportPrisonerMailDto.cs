using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto;

[XmlType("Message")]
public class ExportPrisonerMailDto
{
    private string description;

    [XmlElement("Description")]
    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            var revDescription = value.ToCharArray();
            Array.Reverse(revDescription);
            description = new string(revDescription);
        }
    }
}

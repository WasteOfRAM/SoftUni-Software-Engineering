using System.Xml.Serialization;

namespace Theatre.DataProcessor.ExportDto;

[XmlType("Play")]
public class ExportPlayDto
{
    [XmlAttribute]
    public string Title { get; set; } = null!;

    [XmlAttribute]
    public string Duration { get; set; } = null!;

    [XmlAttribute]
    public string Rating { get; set; } = null!;

    [XmlAttribute]
    public string Genre { get; set; } = null!;

    [XmlArray]
    public ExportActorDto[] Actors { get; set; } = null!;
}

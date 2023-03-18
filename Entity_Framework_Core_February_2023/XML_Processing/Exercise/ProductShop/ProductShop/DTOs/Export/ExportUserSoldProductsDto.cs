using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class ExportUserSoldProductsDto
    {
        public ExportUserSoldProductsDto()
        {
            this.SoldProducts = new List<ExportSoldProductsDto>();
        }

        [XmlElement("firstName")]
        public string FirstName { get; set; } = null!;

        [XmlElement("lastName")]
        public string LastName { get; set; } = null!;

        [XmlArray("soldProducts")]
        public List<ExportSoldProductsDto> SoldProducts { get; set; }
    }
}

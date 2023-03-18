using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    public class ExportUsersAndProductsDto
    {
        public ExportUsersAndProductsDto()
        {
            this.Users = new List<ExportUserDto>();
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public List<ExportUserDto> Users { get; set; }
    }
}

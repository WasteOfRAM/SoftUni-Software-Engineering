using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    public class ExportSoldProductsAndCopuntDto
    {
        public ExportSoldProductsAndCopuntDto()
        {
            this.Products = new List<ExportSoldProductsDto>();
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<ExportSoldProductsDto> Products { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto;

[XmlType("Truck")]
public class TruckInportDto
{
    [Required]
    [XmlElement("RegistrationNumber")]
    [RegularExpression(@"^[A-Z]{2}\d{4}[A-Z]{2}$")]
    public string RegistrationNumber { get; set; } = null!;

    [Required]
    [MaxLength(17)]
    [MinLength(17)]
    [XmlElement("VinNumber")]
    public string VinNumber { get; set; } = null!;

    [Required]
    [Range(950, 1420)]
    [XmlElement("TankCapacity")]
    public int TankCapacity { get; set; }

    [Required]
    [Range(5000, 29000)]
    [XmlElement("CargoCapacity")]
    public int CargoCapacity { get; set; }

    [Required]
    [XmlElement("CategoryType")]
    public int CategoryType { get; set; }

    [Required]
    [XmlElement("MakeType")]
    public int MakeType { get; set; }
}

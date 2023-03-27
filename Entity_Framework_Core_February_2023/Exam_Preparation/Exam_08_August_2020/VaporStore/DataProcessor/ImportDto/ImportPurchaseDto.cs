﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;
using VaporStore.Data.Models;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ImportDto;

[XmlType("Purchase")]
public class ImportPurchaseDto
{
    [XmlAttribute("title")]
    [Required]
    public string Title { get; set; } = null!;

    [XmlElement("Type")]
    [Required]
    public string Type { get; set; } = null!;

    [XmlElement("Key")]
    [Required]
    [RegularExpression(ValidationConstraints.PurchaseProductKeyRegexPatern)]
    public string ProductKey { get; set; } = null!;

    [XmlElement("Card")]
    [Required]
    [RegularExpression(ValidationConstraints.CardNumberRegecPatern)]
    public string Card { get; set; } = null!;

    [XmlElement("Date")]
    [Required]
    public string Date { get; set; } = null!;
}

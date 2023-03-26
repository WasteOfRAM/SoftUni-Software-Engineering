﻿using System.ComponentModel.DataAnnotations;

namespace Trucks.DataProcessor.ImportDto;

public class ImportClientDto
{
    public ImportClientDto()
    {
        this.Trucks = new HashSet<int>();
    }

    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    public string Nationality { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    public ICollection<int> Trucks { get; set; }
}

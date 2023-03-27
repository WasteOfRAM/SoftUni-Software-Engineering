﻿using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models;

public class Tag
{
    public Tag()
    {
        this.GameTags = new HashSet<GameTag>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ICollection<GameTag> GameTags { get; set; }
}

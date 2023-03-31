using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models;

public class Shell
{
    public Shell()
    {
        this.Guns = new HashSet<Gun>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public double ShellWeight { get; set; }

    [Required]
    [MaxLength(ValidationConstraints.CaliberMax)]
    public string Caliber { get; set; } = null!;

    public virtual ICollection<Gun> Guns { get; set; }
}

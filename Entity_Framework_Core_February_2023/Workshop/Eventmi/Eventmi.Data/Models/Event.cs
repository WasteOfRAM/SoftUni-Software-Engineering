using System.ComponentModel.DataAnnotations;

namespace Eventmi.Core.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [StringLength(100)]
        public string Place { get; set; } = null!;
    }
}

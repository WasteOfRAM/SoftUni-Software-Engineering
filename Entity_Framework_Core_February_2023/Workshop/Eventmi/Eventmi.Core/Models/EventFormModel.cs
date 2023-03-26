using System.ComponentModel.DataAnnotations;

namespace Eventmi.Core.Models
{
    public class EventFormModel
    {
        
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1} chracters.")]
        public string Name { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public DateTime Start { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public DateTime End { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1} chracters.")]
        public string Place { get; set; } = null!;
    }
}

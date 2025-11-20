using System.ComponentModel.DataAnnotations;
using Bugify.API.Models.Domain;

namespace Bugify.API.Models.DTO
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title has to be a minimum of 5 characters!")]
        [MaxLength(25, ErrorMessage = "Title has to be a maximum of 25 characters!")]
        [Display(Name ="Title")]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Description has to be a maximum of 100 characters!")]
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }
    }
}

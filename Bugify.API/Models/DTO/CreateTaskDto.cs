using Bugify.API.Models.Domain;

namespace Bugify.API.Models.DTO
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }
    }
}

using Bugify.API.Models.Domain;

namespace Bugify.API.Models.DTO
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }
        public string ProgressName { get; set; } = string.Empty;


    }
}

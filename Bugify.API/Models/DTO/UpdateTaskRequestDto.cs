namespace Bugify.API.Models.DTO
{
    public class UpdateTaskRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }

    }
}

namespace Bugify.API.Models.Domain
{
    public class AddTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskProgress Progress { get; set; }
        public Guid ProgressId { get; set; }

    }
}

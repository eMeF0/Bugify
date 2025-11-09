namespace Bugify.UI.Models
{
    public class CreateTaskViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }
    }

    public class TaskProgress
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}

using Microsoft.AspNetCore.Mvc;

namespace Bugify.UI.Models
{
    public class TaskListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProgressId { get; set; }
        public string ProgressName { get; set; } = string.Empty;
    }

    public class TaskColumnViewModel
    {
        public Guid ProgressId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TaskListViewModel> Tasks { get; set; } = new();
    }
    public class TaskBoardPageViewModel
    {
        public List<TaskColumnViewModel> Columns { get; set; } = new();
    }
}

using Bugify.API.Models.Domain;

namespace Bugify.API.Repositories
{
    public interface ITaskRepository
    {
        Task<AddTask> CreateAsync(AddTask task);
        Task<List<AddTask>> GetAllTasksAsync();
        Task<AddTask?> GetTaskByIdAsync(Guid id);
        Task<AddTask?> UpdateAsync(Guid id, AddTask task);
        Task<AddTask?> DeleteAsync(Guid id);
    }
}

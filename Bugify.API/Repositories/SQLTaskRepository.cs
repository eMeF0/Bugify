using Bugify.API.Data;
using Bugify.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bugify.API.Repositories
{
    public class SQLTaskRepository : ITaskRepository
    {
        private readonly BugifyDbContext _dbContext;

        public SQLTaskRepository(BugifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddTask> CreateAsync(AddTask task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<List<AddTask>> GetAllTasksAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<AddTask?> GetTaskByIdAsync(Guid id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AddTask?> UpdateAsync(Guid id, AddTask task)
        {
            var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingTask is null)
                return null;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.ProgressId = task.ProgressId;
            await _dbContext.SaveChangesAsync();
            return existingTask;

        }

        public async Task<AddTask?> DeleteAsync(Guid id)
        {
            var existingTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingTask is null)
                return null;
            _dbContext.Tasks.Remove(existingTask);
            await _dbContext.SaveChangesAsync();
            return existingTask;
        }
    }
}

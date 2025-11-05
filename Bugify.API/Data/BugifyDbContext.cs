using Bugify.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bugify.API.Data
{
    public class BugifyDbContext : DbContext
    {
        public BugifyDbContext(DbContextOptions<BugifyDbContext> options) : base(options)
        {
            
        }
        public DbSet<AddTask> Tasks { get; set; }
        public DbSet<TaskProgress> TaskProgresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var TaskProgress = new List<TaskProgress>()
            {
                new TaskProgress()
                {
                    Id = Guid.Parse("759c64cb-9639-4218-bd6f-e68718429075"),
                    Name = "NotStarted"
                },
                new TaskProgress()
                {
                    Id = Guid.Parse("915ea79b-f8fa-4f1a-bf02-e9d73faf14a6"),
                    Name = "InProgress"
                },
                new TaskProgress()
                {
                    Id = Guid.Parse("852a80bb-a5d1-4ebd-8959-c115a0ddee68"),
                    Name = "Completed"
                },
                new TaskProgress()
                {
                    Id = Guid.Parse("c99a883d-51e3-4738-9ee6-c6ad53d73b37"),
                    Name = "OnHold"
                },
                new TaskProgress()
                {
                    Id = Guid.Parse("86704552-8451-4916-8494-b0dfd3490f44"),
                    Name = "Cancelled"
                }
            };
            modelBuilder.Entity<TaskProgress>().HasData(TaskProgress);

            modelBuilder.Entity<AddTask>()
                .HasOne(x => x.Progress)
                .WithMany()
                .HasForeignKey(x => x.ProgressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

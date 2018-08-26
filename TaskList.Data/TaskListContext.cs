using System.Data.Entity;
using TaskList.Data.Maps;
using TaskList.Domain.Models;

namespace TaskList.Data
{
    public class TaskListContext : DbContext
    {
        public TaskListContext() : base("TaskList")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaskListContext, TaskList.Data.Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TaskMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}

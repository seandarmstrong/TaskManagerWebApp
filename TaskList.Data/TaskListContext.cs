using TaskList.Domain.Models;
using System.Data.Entity;
using TaskList.Data;
using TaskList.Data.Maps;

namespace TaskList.Data
{
    public class TaskListContext : DbContext
    {
        public TaskListContext() : base("TaskList")
        {
            Database.SetInitializer(new TaskListInitializer());
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

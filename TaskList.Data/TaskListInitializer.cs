using System.Data.Entity;

namespace TaskList.Data
{
    class TaskListInitializer : CreateDatabaseIfNotExists<TaskListContext>
    {
        protected override void Seed(TaskListContext context)
        {

        }
    }
}

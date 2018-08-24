using System;
using System.Collections.Generic;
using TaskList.Data;
using System.Data.Entity;
using TaskList.Domain.Models;

namespace TaskList.Data
{
    class TaskListInitializer : DropCreateDatabaseAlways<TaskListContext>
    {
        protected override void Seed(TaskListContext context)
        {

        }
    }
}

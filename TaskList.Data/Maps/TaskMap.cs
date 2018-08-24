using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TaskList.Domain.Models;

namespace TaskList.Data.Maps
{
    class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}

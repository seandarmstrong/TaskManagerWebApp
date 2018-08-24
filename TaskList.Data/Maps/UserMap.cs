using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TaskList.Domain.Models;


namespace TaskList.Data.Maps
{
    class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(x => x.Tasks).WithRequired(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}

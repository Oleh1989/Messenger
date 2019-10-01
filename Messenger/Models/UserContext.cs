using System.Data.Entity;

namespace Messenger.Models
{
    /// <summary>
    /// 27/09/2019 : Class created
    /// 27/09/2019 : Don't forget to add connection string to web.config file
    /// </summary>
    public class UserContext : DbContext
    {
        static UserContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<UserContext>());            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
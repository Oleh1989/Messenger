using System;
using System.Data.Entity;
using Messenger.Encryption;

namespace Messenger.Models
{
    /// <summary>
    /// 27/09/2019 : Class created
    /// </summary>
    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            Role admin = new Role { Name = "admin" };
            Role user = new Role { Name = "user" };

            string enryptedPassword = Protector.Encrypt("admin");

            db.Roles.Add(admin); db.Roles.Add(user);

            db.Users.Add(new User
            {
                Id = 1,
                Login = "admin",
                Password = enryptedPassword,
                CreationDate = DateTime.Now,
                Role = admin
            });
            base.Seed(db);
        }
    }
}
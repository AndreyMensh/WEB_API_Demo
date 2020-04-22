using System;
using System.Linq;
using Helpers.Cryptography;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Constants;

namespace WEBAPI.Model.Seeds
{
    public static class SuperAdmin
    {
        public static void Seed(ApplicationDatabaseContext context)
        {
            if (!context.Users.Any(x => x.Username == Constant.SuperAdminUsername))
            {
                var role = context.Roles.FirstOrDefault(x => x.Name == "SuperAdmin");

                if (role != null)
                {
                    var user = new User
                    {
                        Username = Constant.SuperAdminUsername,
                        PasswordHash = PasswordHasher.HashPassword(Constant.SuperAdminPassword),
                        Email = Constant.SuperAdminUsername,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = role.Id
                    };

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }
    }
}

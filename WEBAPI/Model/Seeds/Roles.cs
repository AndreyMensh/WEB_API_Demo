using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Constants;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.Model.Seeds
{
    public static class Roles
    {
        public static void Seed(ApplicationDatabaseContext context)
        {
            foreach (var role in RolesConstant.Roles)
                if (!context.Roles.Any(x => x.Name == role.Name))
                    context.Roles.Add(new Role
                    {
                        Name = role.Name,
                        NormalName = role.NormalName
                    });

            context.SaveChanges();
        }
    }
}

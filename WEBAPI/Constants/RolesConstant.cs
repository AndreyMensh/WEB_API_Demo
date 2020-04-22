using System.Collections.Generic;
using WEBAPI.Enums;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.Constants
{
    public static class RolesConstant
    {
        public static List<Role> Roles = new List<Role>
        {
            new Role{Name = nameof(RoleEnum.SuperAdmin), NormalName = "Super Admin"},
            new Role{Name = nameof(RoleEnum.Head), NormalName = "Владелец"},
            new Role{Name = nameof(RoleEnum.Administrator), NormalName = "Администратор"},
            new Role{Name = nameof(RoleEnum.Worker), NormalName = "Рабочий"}
        };
    }
}

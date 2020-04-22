using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.ViewModels.Role;

namespace WEBAPI.Services.Contracts
{
    public interface IRolesService
    {
        void Create(string name);
        List<RoleViewModel> Get();
        void ClearTokens(int userId);
        void Assign(User user, string roleName);
        void AssignAndUpdate(User user, string roleName);
    }
}

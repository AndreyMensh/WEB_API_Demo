using System.Linq;
using Helpers.SearchModels;
using WEBAPI.Enums;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.ViewModels.User;

namespace WEBAPI.Services.Contracts
{
    public interface IUsersService
    {
        SearchResult<User> AlloweUsers(UserSearchModel searchModel, int userId);
        SearchResult<User> Search(UserSearchModel searchModel);

        CreatedUserViewModel Create(CreateUserViewModel model, RoleEnum role);
        User CreateUser(CreateUserViewModel model, RoleEnum role);
        void Edit(int userId, UpdateOrCreateUserViewModel model);
        void Delete(int id, int companyId);
        UserViewModel Find(string username, int companyId);
        UserViewModel Find(int id, int companyId);

        IQueryable<User> GetUser(int id, int companyId);
        IQueryable<User> GetUser(string username, int companyId);
    }
}

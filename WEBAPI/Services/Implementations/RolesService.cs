using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helpers.Exceptions;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Role;

namespace WEBAPI.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;

        public RolesService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(string name)
        {
            if (_context.Roles.Any(x => x.Name == name)) throw new RoleAlreadyExistExceptions("Role already exist.");

            var role = new Role
            {
                Name = name
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public List<RoleViewModel> Get()
        {
            return _mapper.Map<List<Role>, List<RoleViewModel>>(_context.Roles.ToList());
        }

        public void ClearTokens(int userId)
        {
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(x => x.UserId == userId));
            _context.SaveChanges();
        }

        public void Assign(User user, string roleName)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == roleName);
            if (role == null)
                throw new RoleNotFounExceptions("Role not exist.");

            user.RoleId = role.Id;
        }

        public void AssignAndUpdate(User user, string roleName)
        {
            Assign(user, roleName);

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}

using System.Linq;
using Helpers.SearchModels;
using WEBAPI.Enums;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.SearchModels
{
    public class UserSearchModel : OrderedSearchModel<User>
    {
        public int Id { get; set; }
        public int[] Ids { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatusEnum? UserStatus { get; set; }
        public RoleEnum? Role { get; set; }
        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }

        protected override IQueryable<User> Ordering(IQueryable<User> query)
        {
            switch (Predicate)
            {
                case nameof(Id):
                    return Order(query, x => x.Id);
                case nameof(FirstName):
                    return Order(query, x => x.FirstName);
                case nameof(LastName):
                    return Order(query, x => x.LastName);
                case nameof(UserStatus):
                    return Order(query, x => x.UserStatus);
                case nameof(Role):
                    return Order(query, x => x.Role.Id);
                case nameof(PhoneNumber):
                    return Order(query, x => x.PhoneNumber);
                default:
                    return Order(query, x => x.FirstName);
            }
        }

        public override IQueryable<User> Filter(IQueryable<User> query)
        {
            query = query.Where(x => x.CompanyId == CompanyId);

            if (Ids != null && Ids.Length > 0)
            {
                query = query.Where(x => Ids.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(FirstName)) query = query.Where(x => x.FirstName.ToUpper().Contains(FirstName.ToUpper()));
            if (!string.IsNullOrEmpty(LastName)) query = query.Where(x => x.LastName.ToUpper().Contains(LastName.ToUpper()));
            if (!string.IsNullOrEmpty(PhoneNumber)) query = query.Where(x => x.PhoneNumber.ToUpper().Contains(PhoneNumber.ToUpper()));

            if (UserStatus.HasValue && UserStatus.Value != UserStatusEnum.Any) query = query.Where(x => x.UserStatus == UserStatus);
            if (Role.HasValue && Role.Value != RoleEnum.Any) query = query.Where(x => x.Role.Id == (int)Role);

            return base.Filter(query);
        }

        public SearchResult<User> Find(IQueryable<User> query)
        {
            var view = Filter(query).ToList().AsQueryable();

            return new SearchResult<User>
            {
                Body = Ordering(view).Skip(Skip).Take(Take),
                Count = view.Count()
            };
        }
    }
}

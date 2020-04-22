using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.SearchModels;
using WEBAPI.Enums;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.SearchModels
{
    public class JobSearchModel : OrderedSearchModel<Job>
    {
        public JobSearchModel()
        {
            AllowedUsers = new List<int>();
        }

        public GroupingTypeEnum GroupingType { get; set; }
        public decimal? Sum { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }


        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        public int? UserId { get; set; }

        public DateTime? FromTimeCanSee { get; set; }
        public DateTime? ToTimeCanSee { get; set; }
        public int? DaysCanSee { get; set; }


        public int CompanyId { get; set; }
        public int CurrentUserId { get; set; }
        public List<int> AllowedUsers { get; set; }

        protected override IQueryable<Job> Ordering(IQueryable<Job> query)
        {
            switch (Predicate)
            {
                case nameof(CompanyId):
                    return Order(query, x => x.CompanyId);
                case nameof(Date):
                    return Order(query, x => x.DateStart);
                case nameof(Sum):
                    return Order(query, x => x.Sum);

                default:
                    return Order(query, x => x.DateStart);
            }
        }

        public override IQueryable<Job> Filter(IQueryable<Job> query)
        {
            query = query.Where(x => x.CompanyId == CompanyId);

            if (UserId.HasValue) query = query.Where(x => x.UserId == UserId.Value);
            //if (Date.HasValue) query = query.Where(x => x.DateStart.Date == Date.Value.Date);

            if (AllowedUsers.Count > 0)
            {
                query = query.Where(x => AllowedUsers.Contains(x.UserId));
            } 

            if (DaysCanSee.HasValue && FromTime.HasValue && ToTime.HasValue)
            {
                query = query.Where(x => x.DateStart.TimeOfDay >= DateTime.UtcNow.AddDays(-DaysCanSee.Value).TimeOfDay && x.DateStart.TimeOfDay <= DateTime.UtcNow.TimeOfDay);
                query = query.Where(x => x.DateStart.TimeOfDay >= FromTimeCanSee.Value.TimeOfDay && x.DateStart.TimeOfDay <= FromTimeCanSee.Value.TimeOfDay);
            }

            if (DateStart.HasValue && DateEnd.HasValue)
            {
                query = query.Where(x => x.DateStart.Date >= DateStart.Value.ToUniversalTime().Date);
                query = query.Where(x => x.DateStart.Date <= DateEnd.Value.ToUniversalTime().Date);
            }
            else
            {
                if (DateStart.HasValue) query = query.Where(x => x.DateStart.Date == DateStart.Value.ToUniversalTime().Date);
                if (DateEnd.HasValue) query = query.Where(x => x.DateStart.Date == DateEnd.Value.ToUniversalTime().Date);
            }


            if (FromTime.HasValue) query = query.Where(x => x.DateStart.TimeOfDay >= FromTime.Value.ToUniversalTime().TimeOfDay);
            if (ToTime.HasValue) query = query.Where(x => x.DateStart.TimeOfDay <= ToTime.Value.ToUniversalTime().TimeOfDay);

            return base.Filter(query);
        }

        public SearchResult<Job> Find(IQueryable<Job> query)
        {
            var view = Filter(query).ToList().AsQueryable();

            const int take = 1000;
            return new SearchResult<Job>
            {
                Body = Ordering(view).Skip(Skip).Take(take),
                //Body = Ordering(view).Skip(Skip).Take(Take),
                Count = view.Count()
            };
        }
    }
}

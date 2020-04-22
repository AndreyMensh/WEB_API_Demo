using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Model.DatabaseModels
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

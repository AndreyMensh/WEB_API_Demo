using System;

namespace WEBAPI.Model.DatabaseModels
{
    public class TrustedIpAddress
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

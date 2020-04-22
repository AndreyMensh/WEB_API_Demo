using System;

namespace WEBAPI.Model.DatabaseModels
{
    public class Break
    {
        public int Id { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public bool Enabled { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}

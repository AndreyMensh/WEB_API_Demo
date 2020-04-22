using System;

namespace WEBAPI.Model.DatabaseModels
{
    public class CompanySettings
    {
        public CompanySettings()
        {
            BreakTimeMinutes = 15;
            SubtractBreakWorkMinutes = 15;
            MaximumWorkMinutes = 60;
        }

        public int Id { get; set; }


        public bool ObjectRequired { get; set; }
        public bool WorkTypeRequired { get; set; }
        public bool WorkRequired { get; set; }
        public bool EditCommentRequired { get; set; }


        public bool WorkAtNigth { get; set; }
        public DateTime? FromWork { get; set; }
        public DateTime? ToWork { get; set; }
        public int MaximumWorkMinutes { get; set; }


        public int BreakTimeMinutes { get; set; }
        public int SubtractBreakWorkMinutes { get; set; }
        public bool AutomaticBreak { get; set; }


        public bool GpsRequired { get; set; }
        public bool ActRequired { get; set; }
        public bool SumRequired { get; set; }

        public bool ContactNameRequired { get; set; }


        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

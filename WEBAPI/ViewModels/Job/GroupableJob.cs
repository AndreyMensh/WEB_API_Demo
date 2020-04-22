using System.Collections.Generic;

namespace WEBAPI.ViewModels.Job
{
    public class GroupableJob
    {
        public string GroupingEntity { get; set; }
        public string GroupingEntityValue { get; set; }


        public DurationViewModel FullDuration { get; set; }
        public DurationViewModel JobDuration { get; set; }
        public DurationViewModel BreakDuration { get; set; }


        public List<JobViewModel> Jobs { get; set; }
    }
}

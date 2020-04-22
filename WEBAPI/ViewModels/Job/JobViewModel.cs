using System;
using System.Collections.Generic;
using WEBAPI.ViewModels.Act;
using WEBAPI.ViewModels.Break;
using WEBAPI.ViewModels.Location;
using WEBAPI.ViewModels.User;

namespace WEBAPI.ViewModels.Job
{
    public class JobViewModel : UpdateJobViewModel
    {
        public DurationViewModel Duration { get; set; }
        public DurationViewModel BreakDuration { get; set; }
        public DurationViewModel JobDuration { get; set; }



        public List<BreakViewModel> Breaks { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public bool DateStartManuallyChanged { get; set; }
        public bool DateEndManuallyChanged { get; set; }


        public UserViewModel User { get; set; }

        public LocationViewModel StartLocation { get; set; }
        public LocationViewModel EndLocation { get; set; }

        public List<ActViewModel> Acts { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }

    public class DurationViewModel
    {
        public double Minutes { get; set; }
        public double Hours { get; set; }
        public double AllMinutes { get; set; }
    }
}
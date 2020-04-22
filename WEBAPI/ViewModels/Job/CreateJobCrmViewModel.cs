using System;

namespace WEBAPI.ViewModels.Job
{
    public class CreateJobCrmViewModel : CreateJobViewModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}

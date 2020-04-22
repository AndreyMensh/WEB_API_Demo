using System;
using WEBAPI.Enums;
using WEBAPI.ViewModels.Location;
using WEBAPI.ViewModels.User;

namespace WEBAPI.ViewModels.Job
{
    public class UpdateJobViewModel 
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public JobStatusEnum JobStatus { get; set; }

        public decimal Sum { get; set; }
        public string Signature { get; set; }
        public string Photo { get; set; }
        public string SignerPhoto { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string CheckPhoto { get; set; }


        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public bool DateStartManuallyChanged { get; set; }
        public bool DateEndManuallyChanged { get; set; }

        public string Comment { get; set; }
        public string ManagerComment { get; set; }
        public string Answer { get; set; }

        public CreateLocationViewModel EndLocation { get; set; }

        public bool IsFromCrm { get; set; }

        public int UserId { get; set; }

        public UserViewModel User { get; set; }
    }
}

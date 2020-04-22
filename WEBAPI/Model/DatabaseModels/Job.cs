using System;
using System.Collections.Generic;
using WEBAPI.Enums;

namespace WEBAPI.Model.DatabaseModels
{
    public class Job
    {
        public int Id { get; set; }

        public JobStatusEnum JobStatus { get; set; }

        public int? ApproverId { get; set; }
        public DateTime ApproveDateTime { get; set; }

        public decimal Sum { get; set; }
        public string Signature { get; set; }
        public string Photo { get; set; }
        public string SignerPhoto { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string CheckPhoto { get; set; }
        

        public string Comment { get; set; }
        public string ManagerComment { get; set; }
        public string Answer { get; set; }


        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }


        public bool DateStartManuallyChanged { get; set; }
        public bool DateEndManuallyChanged { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }


        public int? StartLocationId { get; set; }
        public Location StartLocation { get; set; }

        public int? EndLocationId { get; set; }
        public Location EndLocation { get; set; }


        public List<Act> Acts { get; set; }
        public List<Break> Breaks { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CompanyId { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }
}

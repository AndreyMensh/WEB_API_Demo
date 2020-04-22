using System.Collections.Generic;
using WEBAPI.ViewModels.Act;
using WEBAPI.ViewModels.Location;

namespace WEBAPI.ViewModels.Job
{
    public class CreateJobViewModel
    {

        public decimal Sum { get; set; }
        public string Signature { get; set; }
        public string Photo { get; set; }
        public string SignerPhoto { get; set; }
        public string ContactEmail { get; set; }
        public string CheckPhoto { get; set; }


        public string Comment { get; set; }
        public string ManagerComment { get; set; }


        public CreateLocationViewModel StartLocation { get; set; }
        public CreateLocationViewModel EndLocation { get; set; }


        public List<CreateActViewModel> Acts { get; set; }

        public int UserId { get; set; }
    }
}

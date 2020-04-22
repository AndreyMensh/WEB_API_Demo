using System.Collections.Generic;

namespace WEBAPI.Model.DatabaseModels
{
    public class Location
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }

        public List<Job> StartLocations { get; set; }
        public List<Job> EndLocations { get; set; }
    }
}

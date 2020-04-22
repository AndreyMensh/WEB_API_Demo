using System;

namespace WEBAPI.Model.DatabaseModels
{
    public class Act
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}

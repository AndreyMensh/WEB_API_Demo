using System;
namespace WEBAPI.Model.DatabaseModels
{
    public class Error
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}

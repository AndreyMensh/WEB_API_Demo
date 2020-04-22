namespace WEBAPI.ViewModels.TableSettings
{
    public class UpdateTableSettingsViewModel
    {
        public bool Worker { get; set; }
        public bool Date { get; set; }
        public bool Start { get; set; }
        public bool End { get; set; }
        public bool Duration { get; set; }
        public bool Break { get; set; }
        public bool GpsStart { get; set; }
        public bool GpsEnd { get; set; }
        public bool Comment { get; set; }
        public bool Summ { get; set; }
        public bool AutomaticRefresh { get; set; }

        public bool Photo { get; set; }
        public bool Act { get; set; }
        public bool Sign { get; set; }
        public bool Email { get; set; }
        public bool Phone { get; set; }
        public bool ManagerComment { get; set; }


        public bool GroupByWorker { get; set; }
        public bool GroupByDate { get; set; }


        public int UserId { get; set; }
    }
}

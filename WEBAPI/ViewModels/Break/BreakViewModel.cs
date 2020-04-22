using System;

namespace WEBAPI.ViewModels.Break
{
    public class BreakViewModel : CreateBreakViewModel
    {
        public int Id { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}

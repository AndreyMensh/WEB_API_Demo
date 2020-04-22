using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.ViewModels.Job;

namespace WEBAPI.Mapper
{
    public static class DurationMapper
    {
        public static DurationViewModel FullDuration(Job src)
        {
            try
            {
                if (!src.DateEnd.HasValue)
                {
                    return new DurationViewModel
                    {
                        AllMinutes = Math.Floor((DateTime.UtcNow - src.DateStart).TotalMinutes),
                        Hours = Math.Floor((DateTime.UtcNow - src.DateStart).TotalHours),
                        Minutes = Math.Floor((DateTime.UtcNow - src.DateStart).TotalMinutes) -
                                  Math.Floor((DateTime.UtcNow - src.DateStart).TotalHours) * 60
                    };
                }
                var result = new DurationViewModel
                {
                    AllMinutes = Math.Floor((src.DateEnd.Value - src.DateStart).TotalMinutes),
                    Hours = Math.Floor((src.DateEnd.Value - src.DateStart).TotalHours),
                    Minutes = Math.Floor((src.DateEnd.Value - src.DateStart).TotalMinutes) -
                              Math.Floor((src.DateEnd.Value - src.DateStart).TotalHours) * 60
                };
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static DurationViewModel BreakDuration(Job src)
        {
            try
            {
                return new DurationViewModel
                {
                    AllMinutes = Math.Floor(src.Breaks.Where(x => !x.Enabled).Sum(x => (x.DateEnd - x.DateStart).Value.TotalMinutes)),
                    Hours = Math.Floor(src.Breaks.Where(x => !x.Enabled).Sum(x => (x.DateEnd - x.DateStart).Value.TotalHours)),
                    Minutes = Math.Floor(
                        Math.Floor(src.Breaks.Where(x => !x.Enabled).Sum(x => (x.DateEnd - x.DateStart).Value.TotalMinutes)) -
                        Math.Floor(src.Breaks.Where(x => !x.Enabled).Sum(x => (x.DateEnd - x.DateStart).Value.TotalHours)) * 60)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static DurationViewModel JobDuration(Job src)
        {
            try
            {
                var fullDuration = FullDuration(src);
                var breakDuration = BreakDuration(src);

                return new DurationViewModel
                {
                    AllMinutes = fullDuration.AllMinutes - breakDuration.AllMinutes,
                    Hours = Math.Floor((fullDuration.AllMinutes - breakDuration.AllMinutes) / 60),
                    Minutes = Math.Floor((fullDuration.AllMinutes - breakDuration.AllMinutes)) - Math.Floor((fullDuration.AllMinutes - breakDuration.AllMinutes) / 60) * 60
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

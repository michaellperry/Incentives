using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incentives.ViewModels
{
    public class ActivityViewModel
    {
        public IEnumerable<CalendarWeekViewModel> Weeks
        {
            get
            {
                DateTime quarterStart = new DateTime(2012, 12, 30);
                for (int week = 0; week < 14; ++week)
                {
                    yield return new CalendarWeekViewModel(quarterStart.AddDays(7.0 * (double)week));
                }
            }
        }
    }
}

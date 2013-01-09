using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incentives.ViewModels
{
    public class CalendarWeekViewModel
    {
        private readonly DateTime _weekStart;

        public CalendarWeekViewModel(DateTime weekStart)
        {
            _weekStart = weekStart;
        }

        public CalendarDayViewModel Sunday
        {
            get { return new CalendarDayViewModel(_weekStart); }
        }

        public CalendarDayViewModel Monday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(1.0)); }
        }

        public CalendarDayViewModel Tuesday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(2.0)); }
        }

        public CalendarDayViewModel Wednesday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(3.0)); }
        }

        public CalendarDayViewModel Thursday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(4.0)); }
        }

        public CalendarDayViewModel Friday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(5.0)); }
        }

        public CalendarDayViewModel Saturday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(6.0)); }
        }
    }
}

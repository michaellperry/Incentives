using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class CalendarWeekViewModel
    {
        private readonly DateTime _weekStart;
        private readonly ActivitySelection _activitySelection;

        public CalendarWeekViewModel(DateTime weekStart, ActivitySelection activitySelection)
        {
            _weekStart = weekStart;
            _activitySelection = activitySelection;
        }

        public CalendarDayViewModel Sunday
        {
            get { return new CalendarDayViewModel(_weekStart, _activitySelection); }
        }

        public CalendarDayViewModel Monday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(1.0), _activitySelection); }
        }

        public CalendarDayViewModel Tuesday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(2.0), _activitySelection); }
        }

        public CalendarDayViewModel Wednesday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(3.0), _activitySelection); }
        }

        public CalendarDayViewModel Thursday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(4.0), _activitySelection); }
        }

        public CalendarDayViewModel Friday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(5.0), _activitySelection); }
        }

        public CalendarDayViewModel Saturday
        {
            get { return new CalendarDayViewModel(_weekStart.AddDays(6.0), _activitySelection); }
        }
    }
}

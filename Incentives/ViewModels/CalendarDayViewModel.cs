using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incentives.ViewModels
{
    public class CalendarDayViewModel
    {
        private readonly DateTime _day;

        public CalendarDayViewModel(DateTime day)
        {
            _day = day;
        }

        public int Day
        {
            get { return _day.Day; }
        }

        public int Month
        {
            get { return _day.Month; }
        }
    }
}

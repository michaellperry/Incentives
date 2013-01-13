using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class CalendarDayViewModel
    {
        private readonly DateTime _date;
        private readonly ActivitySelection _activitySelection;
        
        public CalendarDayViewModel(DateTime date, ActivitySelection activitySelection)
        {
            _date = date;
            _activitySelection = activitySelection;
        }

        public int Day
        {
            get { return _date.Day; }
        }

        public int Month
        {
            get { return _date.Month; }
        }

        public bool IsSelected
        {
            get { return _activitySelection.Date == _date; }
            set
            {
                _activitySelection.Date = value == true
                    ? _date
                    : (DateTime?)null;
            }
        }
    }
}

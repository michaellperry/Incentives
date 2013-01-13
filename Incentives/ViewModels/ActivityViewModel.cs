using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpdateControls.Correspondence;
using Incentives.Model;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class ActivityViewModel
    {
        private readonly Community _community;
        private readonly Individual _individual;
        private readonly Quarter _quarter;
        private readonly CategorySelection _categorySelection;
        
        public ActivityViewModel(Community community, Individual individual, Quarter quarter, CategorySelection categorySelection)
        {
            _community = community;
            _individual = individual;
            _quarter = quarter;
            _categorySelection = categorySelection;
        }

        public IEnumerable<CalendarWeekViewModel> Weeks
        {
            get
            {
                DateTime quarterStart = _quarter.StartDate.AddDays(-(int)_quarter.StartDate.DayOfWeek);
                for (int week = 0; week < 14; ++week)
                {
                    yield return new CalendarWeekViewModel(quarterStart.AddDays(7.0 * (double)week));
                }
            }
        }

        public string Category
        {
            get
            {
                if (_categorySelection.Category == null)
                    return null;

                return _categorySelection.Category.Description;
            }
        }

        public IEnumerable<ActivityOptionViewModel> Options
        {
            get
            {
                return
                    from reward in _quarter.Rewards
                    where reward.Definition != null
                       && reward.Definition.Category == _categorySelection.Category
                       && reward.Definition.Ordinal.Candidates.Any()
                    orderby reward.Definition.Ordinal.Value
                    select new ActivityOptionViewModel(reward);
            }
        }
    }
}

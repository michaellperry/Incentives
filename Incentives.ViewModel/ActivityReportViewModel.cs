using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incentives.Model;

namespace Incentives.ViewModel
{
    public class ActivityReportViewModel
    {
        private readonly Profile _profile;

        public ActivityReportViewModel(Profile profile)
        {
            _profile = profile;
        }

        public string Name
        {
            get { return _profile.Name; }
        }

        public IEnumerable<ActivityViewModel> Activities
        {
            get
            {
                return
                    from activity in _profile.Activities
                    orderby activity.ActivityDate
                    select new ActivityViewModel(activity);
            }
        }
    }
}

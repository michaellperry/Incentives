using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incentives.Model;

namespace Incentives.ViewModel
{
    public class ActivityViewModel
    {
        private readonly Activity _activity;

        public ActivityViewModel(Activity activity)
        {
            _activity = activity;
        }

        public DateTime Date
        {
            get { return _activity.ActivityDate; }
        }

        public string Category
        {
            get { return _activity.ActivityReward.Definition.Category.Description; }
        }

        public string Activity
        {
            get { return _activity.ActivityReward.Definition.Description; }
        }

        public string Description
        {
            get { return _activity.Description; }
        }

        public string PointsAvailable
        {
            get
            {
                if (string.IsNullOrEmpty(_activity.ActivityReward.Definition.Qualifier))
                    return _activity.ActivityReward.Points.Value.ToString();
                else
                    return String.Format("{0}/{1}",
                        _activity.ActivityReward.Points.Value,
                        _activity.ActivityReward.Definition.Qualifier.Value);
            }
        }

        public int Amount
        {
            get
            {
                int multiplier = 1;
                if (_activity.Multiplier.Candidates.Any())
                    multiplier = _activity.Multiplier.Value;

                return _activity.ActivityReward.Points * multiplier;
            }
        }
    }
}

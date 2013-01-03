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
            get
            {
                if (_activity.ActivityReward != null &&
                    _activity.ActivityReward.Definition != null &&
                    _activity.ActivityReward.Definition.Category != null)
                    return _activity.ActivityReward.Definition.Category.Description;

                return null;
            }
        }

        public string Activity
        {
            get
            {
                if (_activity.ActivityReward != null &&
                    _activity.ActivityReward.Definition != null)
                    return _activity.ActivityReward.Definition.Description;

                return null;
            }
        }

        public string Description
        {
            get { return _activity.Description; }
        }

        public string PointsAvailable
        {
            get
            {
                if (_activity.ActivityReward != null &&
                    _activity.ActivityReward.Definition != null)
                {
                    if (string.IsNullOrEmpty(_activity.ActivityReward.Definition.Qualifier))
                        return _activity.ActivityReward.Points.Value.ToString();
                    else
                        return String.Format("{0}/{1}",
                            _activity.ActivityReward.Points.Value,
                            _activity.ActivityReward.Definition.Qualifier.Value);
                }

                return null;
            }
        }

        public int Amount
        {
            get
            {
                if (_activity.ActivityReward != null)
                {
                    int multiplier = 1;
                    if (_activity.Multiplier.Candidates.Any())
                        multiplier = _activity.Multiplier.Value;

                    return _activity.ActivityReward.Points * multiplier;
                }

                return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incentives.Model;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class ActivityOptionViewModel
    {
        private readonly ActivityReward _reward;
        private readonly ActivitySelection _activitySelection;
        
        public ActivityOptionViewModel(ActivityReward reward, ActivitySelection activitySelection)
        {
            _reward = reward;
            _activitySelection = activitySelection;
        }

        public int PointValue
        {
            get { return _reward.Points.Value; }
        }

        public string Description
        {
            get { return _reward.Definition.Description; }
        }

        public bool IsSelected
        {
            get { return _activitySelection.ActivityReward == _reward; }
            set
            {
                _activitySelection.ActivityReward = value == false
                    ? null
                    : _reward;
            }
        }
    }
}

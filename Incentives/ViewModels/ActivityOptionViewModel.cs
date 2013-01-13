using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incentives.Model;

namespace Incentives.ViewModels
{
    public class ActivityOptionViewModel
    {
        private readonly ActivityReward _reward;

        public ActivityOptionViewModel(ActivityReward reward)
        {
            _reward = reward;            
        }

        public int PointValue
        {
            get { return _reward.Points.Value; }
        }

        public string Description
        {
            get { return _reward.Definition.Description; }
        }
    }
}

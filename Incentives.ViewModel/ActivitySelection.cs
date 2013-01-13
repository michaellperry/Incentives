using Incentives.Model;
using UpdateControls.Fields;
using System;

namespace Incentives.ViewModel
{
    public class ActivitySelection
    {
        private Independent<Category> _category = new Independent<Category>();
        private Independent<string> _description = new Independent<string>();
        private Independent<int> _quantity = new Independent<int>(1);
        private Independent<ActivityReward> _activityReward = new Independent<ActivityReward>();
        private Independent<DateTime?> _date = new Independent<DateTime?>();

        public Category Category
        {
            get { return _category; }
            set { _category.Value = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description.Value = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity.Value = value; }
        }

        public ActivityReward ActivityReward
        {
            get { return _activityReward; }
            set { _activityReward.Value = value; }
        }

        public DateTime? Date
        {
            get { return _date; }
            set { _date.Value = value; }
        }

        public void Clear()
        {
            Category = null;
            ActivityReward = null;
            Description = null;
            Quantity = 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpdateControls.Correspondence;
using Incentives.Model;
using Incentives.ViewModel;
using System.Windows.Input;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Incentives.ViewModels
{
    public class ActivityViewModel
    {
        private readonly Community _community;
        private readonly Individual _individual;
        private readonly Quarter _quarter;
        private readonly ActivitySelection _activitySelection;
        
        public ActivityViewModel(Community community, Individual individual, Quarter quarter, ActivitySelection activitySelection)
        {
            _community = community;
            _individual = individual;
            _quarter = quarter;
            _activitySelection = activitySelection;
        }

        public IEnumerable<CalendarWeekViewModel> Weeks
        {
            get
            {
                DateTime quarterStart = _quarter.StartDate.AddDays(-(int)_quarter.StartDate.DayOfWeek);
                for (int week = 0; week < 14; ++week)
                {
                    yield return new CalendarWeekViewModel(quarterStart.AddDays(7.0 * (double)week), _activitySelection);
                }
            }
        }

        public string Category
        {
            get
            {
                if (_activitySelection.Category == null)
                    return null;

                return _activitySelection.Category.Description;
            }
        }

        public string Description
        {
            get { return _activitySelection.Description; }
            set { _activitySelection.Description = value; }
        }

        public int Quantity
        {
            get { return _activitySelection.Quantity; }
            set { _activitySelection.Quantity = value; }
        }

        public string Qualifier
        {
            get
            {
                if (_activitySelection.ActivityReward == null)
                    return null;

                return _activitySelection.ActivityReward.Definition.Qualifier;
            }
        }

        public bool PointValueVisible
        {
            get
            {
                if (_activitySelection.ActivityReward == null)
                    return false;

                return _activitySelection.ActivityReward.Points.Candidates.Any();
            }
        }

        public string PointValue
        {
            get
            {
                if (!PointValueVisible)
                    return null;

                int points =
                    _activitySelection.ActivityReward.Points.Value *
                    _activitySelection.Quantity;
                if (points == 0)
                    return "0";
                else
                    return String.Format("+{0}", points);
            }
        }

        public IEnumerable<ActivityOptionViewModel> Options
        {
            get
            {
                return
                    from reward in _quarter.Rewards
                    where reward.Definition.Category == _activitySelection.Category
                       && reward.Definition.Ordinal.Candidates.Any()
                    orderby reward.Definition.Ordinal.Value
                    select new ActivityOptionViewModel(reward, _activitySelection);
            }
        }

        public ICommand Ok
        {
            get
            {
                return MakeCommand
                    .When(() =>
                        _individual.Profiles.Any() &&
                        _activitySelection.Category != null &&
                        _activitySelection.ActivityReward != null &&
                        _activitySelection.ActivityReward.Definition.Category ==
                            _activitySelection.Category &&
                        !String.IsNullOrWhiteSpace(_activitySelection.Description) &&
                        _activitySelection.Quantity >= 1 &&
                        _activitySelection.Date != null)
                    .Do(async delegate
                    {
                        var profile = (await _individual.Profiles.EnsureAsync()).FirstOrDefault();
                        var individualQuarter = await _community.AddFactAsync(new ProfileQuarter(
                            profile,
                            _quarter));
                        await _community.AddFactAsync(new Activity(
                            individualQuarter,
                            _activitySelection.ActivityReward,
                            _activitySelection.Date.Value));

                        // TODO: Yuck.
                        var frame = Window.Current.Content as Frame;
                        frame.GoBack();
                    });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        // TODO: Yuck.
                        var frame = Window.Current.Content as Frame;
                        frame.GoBack();
                    });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Incentives.Model;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class MacroViewModel
    {
        private readonly Category _category;
        private readonly ActivitySelection _activitySelection;
        
        public MacroViewModel(Category category, ActivitySelection activitySelection)
        {
            _category = category;
            _activitySelection = activitySelection;
        }

        public string Label
        {
            get { return _category.Description; }
        }

        public int Points
        {
            get { return 42; }
        }

        public ICommand Select
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        _activitySelection.Category = _category;

                        // TODO: This is dirty.
                        var frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(ActivityPage));
                    });
            }
        }
    }
}

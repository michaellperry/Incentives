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
        private readonly CategorySelection _categorySelection;
        
        public MacroViewModel(Category category, CategorySelection categorySelection)
        {
            _category = category;
            _categorySelection = categorySelection;
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
                        _categorySelection.Category = _category;

                        // TODO: This is dirty.
                        var frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(ActivityPage));
                    });
            }
        }
    }
}

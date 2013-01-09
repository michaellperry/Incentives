using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UpdateControls.XAML;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Incentives.ViewModels
{
    public class MacroViewModel
    {
        private readonly string _label;

        public MacroViewModel(string label)
        {
            _label = label;
        }

        public string Label
        {
            get { return _label; }
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
                        // TODO: This is dirty.
                        var frame = Window.Current.Content as Frame;
                        frame.Navigate(typeof(ActivityPage));
                    });
            }
        }
    }
}

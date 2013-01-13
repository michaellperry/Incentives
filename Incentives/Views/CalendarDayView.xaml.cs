using Incentives.ViewModels;
using UpdateControls;
using UpdateControls.XAML;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Incentives.Views
{
    public sealed partial class CalendarDayView : UserControl
    {
        public CalendarDayView()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var scheduler = UpdateScheduler.Begin();

            var viewModel = ForView.Unwrap<CalendarDayViewModel>(DataContext);

            if (viewModel != null)
                viewModel.IsSelected = true;

            foreach (var update in scheduler.End())
                update.UpdateNow();
        }
    }
}

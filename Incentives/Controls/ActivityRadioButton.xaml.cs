using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Incentives.Controls
{
    public sealed partial class ActivityRadioButton : UserControl
    {
        public static DependencyProperty LabelProperty =
            DependencyProperty.Register(
                "Label",
                typeof(string),
                typeof(ActivityRadioButton),
                new PropertyMetadata("Activity", LabelChanged));

        public static DependencyProperty PointValueProperty =
            DependencyProperty.Register(
                "PointValue",
                typeof(int),
                typeof(ActivityRadioButton),
                new PropertyMetadata(1, PointValueChanged));

        public ActivityRadioButton()
        {
            this.InitializeComponent();
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public int PointValue
        {
            get { return (int)GetValue(PointValueProperty); }
            set { SetValue(PointValueProperty, value); }
        }

        private static void LabelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as ActivityRadioButton;
            if (control != null)
                control.RadioButton.Content = args.NewValue;
        }

        private static void PointValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as ActivityRadioButton;
            if (control != null)
                control.PointIndicator.Text = String.Format("+{0}", args.NewValue);
        }
    }
}

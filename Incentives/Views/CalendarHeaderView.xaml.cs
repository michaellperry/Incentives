﻿using System;
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

namespace Incentives.Views
{
    public sealed partial class CalendarHeaderView : UserControl
    {
        public CalendarHeaderView()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        	// TODO: Another place where the dirty appears.
			var frame = Window.Current.Content as Frame;
			frame.GoBack();
        }
    }
}
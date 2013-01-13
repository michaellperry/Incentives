using System;
using System.ComponentModel;
using System.Linq;
using Incentives.ViewModel;
using UpdateControls.XAML;

namespace Incentives.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SynchronizationService _synchronizationService = new SynchronizationService();
        private readonly CategorySelection _categorySelection = new CategorySelection();

        public ViewModelLocator()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _synchronizationService.Initialize();
        }

        public object Main
        {
            get
            {
                return ViewModel(() =>
                    _synchronizationService.Individual == null ||
                    _synchronizationService.Company == null
                    ? null :
                    new MainViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Individual,
                        _synchronizationService.Company,
                        _categorySelection));
            }
        }

        public object Activity
        {
            get
            {
                return ViewModel(() =>
                    _synchronizationService.Individual == null ||
                    _synchronizationService.Company == null
                    ? null :
                    new ActivityViewModel(
                        _synchronizationService.Community,
                        _synchronizationService.Individual,
                        _synchronizationService.Quarter,
                        _categorySelection));
            }
        }
    }
}

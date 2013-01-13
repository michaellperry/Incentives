using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Incentives.Model;
using UpdateControls.Correspondence;
using UpdateControls.XAML;
using Incentives.ViewModel;

namespace Incentives.ViewModels
{
    public class MainViewModel
    {
        private readonly Community _community;
        private readonly Individual _individual;
        private readonly Company _company;
        private readonly CategorySelection _categorySelection;
        
        public MainViewModel(Community community, Individual individual, Company company, CategorySelection categorySelection)
        {
            _community = community;
            _individual = individual;
            _company = company;
            _categorySelection = categorySelection;
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                return _community.LastException == null
                    ? String.Empty
                    : _community.LastException.Message;
            }
        }

        public IEnumerable<MacroViewModel> Categories
        {
            get
            {
                return
                    from category in _company.Categories
                    orderby category.Ordinal.Value
                    select new MacroViewModel(
                        category,
                        _categorySelection);
            }
        }
    }
}

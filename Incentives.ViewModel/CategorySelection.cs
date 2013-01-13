using Incentives.Model;
using UpdateControls.Fields;

namespace Incentives.ViewModel
{
    public class CategorySelection
    {
        private Independent<Category> _category = new Independent<Category>();

        public Category Category
        {
            get { return _category; }
            set { _category.Value = value; }
        }
    }
}

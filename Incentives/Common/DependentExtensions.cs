using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls;

namespace Incentives.Common
{
    public static class DependentExtensions
    {
        class DependentUpdatable : IUpdatable
        {
            private Dependent _dependent;

            public DependentUpdatable(Dependent dependent)
            {
                _dependent = dependent;
            }

            public void UpdateNow()
            {
                _dependent.OnGet();
            }
        }

        public static void UpdateWhenNecessary(this Dependent dependent)
        {
            DependentUpdatable updatable = new DependentUpdatable(dependent);
            dependent.Invalidated += () => UpdateScheduler.ScheduleUpdate(updatable);
            dependent.OnGet();
        }
    }
}

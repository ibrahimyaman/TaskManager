using System.Collections.Generic;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class DailyPlanDetail : IEntity
    {
        public int Id { get; set; }
        public int DailyPlanId { get; set; }
        public string Description { get; set; }

        public virtual DailyPlan DailyPlan { get; set; }
    }
}
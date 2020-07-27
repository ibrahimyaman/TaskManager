using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class MonthlyPlanDetail : IEntity
    {
        public int Id { get; set; }
        public int MonthlyPlanId { get; set; }
        public string Description { get; set; }

        public virtual MonthlyPlan MonthlyPlan { get; set; }
    }
}
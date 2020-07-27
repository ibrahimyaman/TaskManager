using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class WeeklyPlanDetail : IEntity
    {
        public int Id { get; set; }
        public int WeeklyPlanId { get; set; }
        public string Description { get; set; }

        public virtual WeeklyPlan WeeklyPlan { get; set; }
    }
}
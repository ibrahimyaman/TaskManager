using System.Collections.Generic;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class ImportanceType : IEntity
    {
        public ImportanceType()
        {
            DailyPlans = new HashSet<DailyPlan>();
            MonthlyPlans = new HashSet<MonthlyPlan>();
            WeeklyPlans = new HashSet<WeeklyPlan>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DailyPlan> DailyPlans { get; set; }
        public virtual ICollection<MonthlyPlan> MonthlyPlans { get; set; }
        public virtual ICollection<WeeklyPlan> WeeklyPlans { get; set; }
    }
}
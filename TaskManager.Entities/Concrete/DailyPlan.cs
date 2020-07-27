using System;
using System.Collections.Generic;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class DailyPlan : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ImportanceTypeId { get; set; }
        public bool IsOver { get => Date.AddDays(1) <= DateTime.Today; }
        public DateTime RegisterDate { get; set; }

        public virtual ImportanceType ImportanceType { get; set; }
        public virtual ICollection<DailyPlanDetail> DailyPlanDetails { get; set; }
    }
}
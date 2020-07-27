using System;
using System.Collections.Generic;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete
{
    public partial class MonthlyPlan : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ImportanceTypeId { get; set; }
        public bool IsOver { get => new DateTime(Year, Month, 1).AddMonths(1) <= DateTime.Today; }
        public DateTime RegisterDate { get; set; }

        public virtual ImportanceType ImportanceType { get; set; }
        public virtual ICollection<MonthlyPlanDetail>  MonthlyPlanDetails { get; set; }
    }
}
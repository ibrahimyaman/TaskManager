using System;
using System.Collections.Generic;
using TaskManager.Core.Entities;
using TaskManager.Core.Extensions;

namespace TaskManager.Entities.Concrete
{
    public partial class WeeklyPlan : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public int ImportanceTypeId { get; set; }
        public bool IsOver { get => DateTime.Now.FirstDateOfWeek(Year, WeekNumber).AddWeeks(1) <= DateTime.Today; }
        public DateTime RegisterDate { get; set; }

        public virtual ImportanceType ImportanceType { get; set; }
        public virtual ICollection<WeeklyPlanDetail> WeeklyPlanDetails { get; set; }
    }
}
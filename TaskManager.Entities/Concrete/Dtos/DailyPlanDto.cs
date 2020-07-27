using System;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class DailyPlanDto : IDto
    {
        public int Id { get; set; }
        public int? WeeklyPlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ImportanceTypeId { get; set; }
    }
}

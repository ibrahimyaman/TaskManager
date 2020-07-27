using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class WeeklyPlanDto : IDto
    {
        public int Id { get; set; }
        public int? MonthlyPlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public int ImportanceTypeId { get; set; }
    }
}

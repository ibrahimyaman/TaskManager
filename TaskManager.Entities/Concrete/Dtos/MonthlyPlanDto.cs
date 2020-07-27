using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class MonthlyPlanDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ImportanceTypeId { get; set; }
    }
}

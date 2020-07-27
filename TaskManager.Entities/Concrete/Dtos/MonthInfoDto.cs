using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class MonthInfoDto : IDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

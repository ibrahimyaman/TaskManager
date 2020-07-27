using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class WeekInfoDto : IDto
    {
        public int Year { get; set; }
        public int Week { get; set; }
    }
}

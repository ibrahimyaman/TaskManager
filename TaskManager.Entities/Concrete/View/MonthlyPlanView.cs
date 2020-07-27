using System;

namespace TaskManager.Entities.Concrete.View
{
    public class MonthlyPlanView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ImportanceTypeId { get; set; }
        public string ImportanceType { get; set; }
        public bool IsOver { get => new DateTime(Year, Month, 1).AddMonths(1) <= DateTime.Today; }
        public DateTime RegisterDate { get; set; }
    }
}

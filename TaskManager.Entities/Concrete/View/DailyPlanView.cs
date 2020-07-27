using System;
using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.View
{
    public class DailyPlanView:IView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int ImportanceTypeId { get; set; }
        public string ImportanceType { get; set; }
        public bool IsOver { get => Date.AddDays(1) <= DateTime.Today; }
        public DateTime RegisterDate { get; set; }
    }
}

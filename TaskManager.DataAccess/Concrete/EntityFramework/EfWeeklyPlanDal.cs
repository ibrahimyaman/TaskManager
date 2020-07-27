using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using TaskManager.Core.DataAccess.EntityFramework;
using TaskManager.DataAccess.Abstract;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.DataAccess.Concrete.EntityFramework
{
    public class EfWeeklyPlanDal : EfEntityRepositoryBase<WeeklyPlan, TaskManagerDbContext>, IWeeklyPlanDal
    {
        public List<WeeklyPlanView> GetListAsView(Expression<Func<WeeklyPlanView, bool>> filter = null)
        {
            using (var context = new TaskManagerDbContext())
            {
                var result = from wp in context.WeeklyPlans
                             join u in context.Users on wp.UserId equals u.Id
                             select new WeeklyPlanView
                             {
                                 Id = wp.Id,
                                 WeekNumber = wp.WeekNumber,
                                 Year = wp.Year,
                                 Description = wp.Description,
                                 ImportanceType = wp.ImportanceType.Description,
                                 ImportanceTypeId = wp.ImportanceTypeId,
                                 Name = wp.Name,
                                 RegisterDate = wp.RegisterDate,
                                 User = u.Name + " " + u.Surname,
                                 UserId = wp.UserId,
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}

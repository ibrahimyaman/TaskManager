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
    public class EfDailyPlanDal : EfEntityRepositoryBase<DailyPlan, TaskManagerDbContext>, IDailyPlanDal
    {
        public List<DailyPlanView> GetListAsView(Expression<Func<DailyPlanView, bool>> filter = null)
        {
            using (var context = new TaskManagerDbContext())
            {
                var result = from dp in context.DailyPlans
                             join u in context.Users on dp.UserId equals u.Id
                             select new DailyPlanView
                             {
                                 Id = dp.Id,
                                 Date = dp.Date,
                                 Description = dp.Description,
                                 ImportanceType = dp.ImportanceType.Description,
                                 ImportanceTypeId = dp.ImportanceTypeId,
                                 Name = dp.Name,
                                 RegisterDate = dp.RegisterDate,
                                 User = u.Name + " " + u.Surname,
                                 UserId = dp.UserId,
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}

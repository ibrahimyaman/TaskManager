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
    public class EfMonthlyPlanDal : EfEntityRepositoryBase<MonthlyPlan, TaskManagerDbContext>, IMonthlyPlanDal
    {
        public List<MonthlyPlanView> GetListAsView(Expression<Func<MonthlyPlanView, bool>> filter = null)
        {
            using (var context = new TaskManagerDbContext())
            {
                var result = from mp in context.MonthlyPlans
                             join u in context.Users on mp.UserId equals u.Id
                             select new MonthlyPlanView
                             {
                                 Id = mp.Id,
                                 Month = mp.Month,
                                 Year = mp.Year,
                                 Description = mp.Description,
                                 ImportanceType = mp.ImportanceType.Description,
                                 ImportanceTypeId = mp.ImportanceTypeId,
                                 Name = mp.Name,
                                 RegisterDate = mp.RegisterDate,
                                 User = u.Name + " " + u.Surname,
                                 UserId = mp.UserId,
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.Core.DataAccess;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.DataAccess.Abstract
{
    public interface IWeeklyPlanDal : IEntityRepository<WeeklyPlan>
    {
        List<WeeklyPlanView> GetListAsView(Expression<Func<WeeklyPlanView, bool>> filter = null);
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManager.Core.DataAccess;
using TaskManager.Entities.Concrete;
using TaskManager.Entities.Concrete.View;

namespace TaskManager.DataAccess.Abstract
{
    public interface IMonthlyPlanDetailDal : IEntityRepository<MonthlyPlanDetail>
    {
        //List<MonthlyPlanView> GetListAsView(Expression<Func<MonthlyPlanView, bool>> filter = null);
    }
}

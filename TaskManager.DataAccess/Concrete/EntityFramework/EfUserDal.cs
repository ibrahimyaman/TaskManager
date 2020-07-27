using System.Collections.Generic;
using TaskManager.Core.DataAccess.EntityFramework;
using TaskManager.Core.Entities.Concrete;
using TaskManager.DataAccess.Abstract;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;
using System.Linq;

namespace TaskManager.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, TaskManagerDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new TaskManagerDbContext())
            {
                var result = from uo in context.UserOperationClaims
                             where uo.UserId == user.Id
                             select uo.OperationClaim;

                return result.ToList();
            }
        }
    }
}

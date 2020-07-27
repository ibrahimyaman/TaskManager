using TaskManager.Core.DataAccess.EntityFramework;
using TaskManager.Core.Entities.Concrete;
using TaskManager.DataAccess.Abstract;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;

namespace TaskManager.DataAccess.Concrete.EntityFramework
{
    public class EfRefreshTokenDal : EfEntityRepositoryBase<RefreshToken, TaskManagerDbContext>, IRefreshTokenDal
    {
    }
}

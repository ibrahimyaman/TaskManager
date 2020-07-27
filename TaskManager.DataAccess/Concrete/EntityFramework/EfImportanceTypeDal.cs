using TaskManager.Core.DataAccess.EntityFramework;
using TaskManager.DataAccess.Abstract;
using TaskManager.DataAccess.Concrete.EntityFramework.Contexts;
using TaskManager.Entities.Concrete;

namespace TaskManager.DataAccess.Concrete.EntityFramework
{
    public class EfImportanceTypeDal: EfEntityRepositoryBase<ImportanceType, TaskManagerDbContext>, IImportanceTypeDal
    {
    }
}

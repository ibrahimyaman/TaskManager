using TaskManager.Core.DataAccess;
using TaskManager.Entities.Concrete;

namespace TaskManager.DataAccess.Abstract
{
    public interface IImportanceTypeDal: IEntityRepository<ImportanceType>
    {
    }
}

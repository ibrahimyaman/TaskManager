using System.Collections.Generic;
using TaskManager.Core.Utilities.Results;
using TaskManager.Entities.Concrete;

namespace TaskManager.Business.Abstract
{
    public interface IImportanceTypeService
    {
        IDataResult<List<ImportanceType>> GetAll();
    }
}

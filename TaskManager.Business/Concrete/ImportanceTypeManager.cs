using System.Collections.Generic;
using System.Linq;
using TaskManager.Business.Abstract;
using TaskManager.Core.Utilities.Results;
using TaskManager.DataAccess.Abstract;
using TaskManager.Entities.Concrete;

namespace TaskManager.Business.Concrete
{
    public class ImportanceTypeManager : IImportanceTypeService
    {
        private IImportanceTypeDal _importanceTypeDal { get; set; }
        public ImportanceTypeManager(IImportanceTypeDal importanceTypeDal)
        {
            _importanceTypeDal = importanceTypeDal;
        }
        public IDataResult<List<ImportanceType>> GetAll()
        {
            return new SuccessDataResult<List<ImportanceType>>(_importanceTypeDal.GetList().ToList());
        }
    }
}

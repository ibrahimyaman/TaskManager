using System.Collections.Generic;
using TaskManager.Core.DataAccess;
using TaskManager.Core.Entities.Concrete;

namespace TaskManager.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}

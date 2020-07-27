using System.Collections.Generic;
using TaskManager.Core.Entities.Concrete;
using TaskManager.Core.Utilities.Results;
using TaskManager.Core.Utilities.Security.Jwt;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByEmail(string eMail);
        IResult Add(User user);
        IResult Update(UserUpdateDto userUpdateDto);
        IResult Update(User user);
        IDataResult<List<OperationClaim>> GetOperationClaims(User user);
        IResult AddRefreshToken(User user, AccessToken accessToken);
    }
}

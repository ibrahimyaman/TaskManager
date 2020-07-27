using TaskManager.Core.Entities.Concrete;
using TaskManager.Core.Utilities.Results;
using TaskManager.Core.Utilities.Security.Jwt;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        IDataResult<User> LoginByEmail(UserLoginDto userLoginDto);
        IResult EpostaExist(string eMail);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<AccessToken> RefreshAccessToken(string refreshToken);
        IResult ChangePassword(UserChangePassordDto userChangePassordDto);
    }
}

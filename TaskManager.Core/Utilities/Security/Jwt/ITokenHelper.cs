using System.Collections.Generic;
using TaskManager.Core.Entities.Concrete;

namespace TaskManager.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        string CreateRefreshToken();
    }
}

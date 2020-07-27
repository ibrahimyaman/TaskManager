using System.Collections.Generic;
using Takas.Business.ValidationRules.FluentValidation;
using TaskManager.Business.Abstract;
using TaskManager.Business.BusinessAspect;
using TaskManager.Business.Constants;
using TaskManager.Core.Aspects.Autofac.Validation;
using TaskManager.Core.Entities.Concrete;
using TaskManager.Core.Extensions;
using TaskManager.Core.Utilities.Results;
using TaskManager.Core.Utilities.Security.Hashing;
using TaskManager.Core.Utilities.Security.Jwt;
using TaskManager.DataAccess.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.Concrete
{
    public class UserManager : IUserService, ISecuredUser
    {
        private IUserDal _userDal { get; set; }
        private IRefreshTokenDal _refreshTokenDal { get; set; }
        public int UserId { get; set; }

        public UserManager(IUserDal userDal, IRefreshTokenDal refreshTokenDal)
        {
            _userDal = userDal;
            _refreshTokenDal = refreshTokenDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }
        [ValidationAspect(typeof(UserUpdateValidator), Priority = 1)]
        [SecuredOperation]
        public IResult Update(UserUpdateDto userUpdateDto)
        {
            var user = _userDal.Get(w => w.Id == UserId);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            user.Name = userUpdateDto.Name;
            user.Surname = userUpdateDto.Surname;
            user.Email = userUpdateDto.Email;

            if (userUpdateDto.Password.IsNullOrWhiteSpace())
            {
                byte[] parolaHash, parolaSalt;
                HashingHelper.CreatePasswordHash(userUpdateDto.Password, out parolaHash, out parolaSalt);
                user.PasswordHash = parolaHash;
                user.PasswordSalt = parolaSalt;
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
        public IResult AddRefreshToken(User user, AccessToken accessToken)
        {
            var refreshToken = new RefreshToken
            {
                ExpirationDate = accessToken.Expiration,
                UserId = user.Id,
                Token = accessToken.RefreshToken,
            };
            _refreshTokenDal.Add(refreshToken);
            return new SuccessResult();
        }

        public IDataResult<User> GetByEmail(string eMail)
        {
            var user = _userDal.Get(w => w.Email == eMail);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(w => w.Id == id);

            if (user == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<List<OperationClaim>> GetOperationClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

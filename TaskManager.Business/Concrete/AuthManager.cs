using System;
using System.Linq;
using Takas.Business.ValidationRules.FluentValidation;
using TaskManager.Business.Abstract;
using TaskManager.Business.BusinessAspect;
using TaskManager.Business.Constants;
using TaskManager.Core.Aspects.Autofac.Logging;
using TaskManager.Core.Aspects.Autofac.Transaction;
using TaskManager.Core.Aspects.Autofac.Validation;
using TaskManager.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using TaskManager.Core.Entities.Concrete;
using TaskManager.Core.Utilities.Results;
using TaskManager.Core.Utilities.Security.Hashing;
using TaskManager.Core.Utilities.Security.Jwt;
using TaskManager.DataAccess.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace TaskManager.Business.Concrete
{
    public class AuthManager : IAuthService, ISecuredUser
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IRefreshTokenDal _refreshTokenDal;
        private IOperationClaimDal _operationClaimDal;
        public int UserId { get; set; }
        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            IRefreshTokenDal refreshTokens,
            IOperationClaimDal operationClaimDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _refreshTokenDal = refreshTokens;
            _operationClaimDal = operationClaimDal;
        }


        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetOperationClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);

            RemoveTimedOutRefreshTokens(user);
            _userService.AddRefreshToken(user, accessToken);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IResult EpostaExist(string eMail)
        {
            var kullaniciKontrol = _userService.GetByEmail(eMail);
            if (kullaniciKontrol.Data != null)
            {
                return new ErrorResult(Messages.RegisteredEmail);
            }
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserLoginDtoValidator), Priority = 1)]
        public IDataResult<User> LoginByEmail(UserLoginDto userLoginDto)
        {
            var kullaniciKontrol = _userService.GetByEmail(userLoginDto.Email);
            return Login(userLoginDto, kullaniciKontrol.Data);
        }
        private IDataResult<User> Login(UserLoginDto kullaniciLoginDto, User user)
        {
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(kullaniciLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }

            return new SuccessDataResult<User>(user);
        }
        [SecuredOperation]
        [TransactionScopeAspect]
        public IDataResult<AccessToken> RefreshAccessToken(string refreshToken)
        {
            var userControl = _userService.GetById(UserId);
            if (userControl.Data == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.InvalidToken);
            }

            var _refreshToken = _refreshTokenDal.Get(w => w.UserId == UserId && w.Token == refreshToken);
            if (_refreshToken == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.InvalidToken);
            }
            else if (!_refreshToken.IsActive)
            {
                _refreshTokenDal.Delete(_refreshToken);
                return new ErrorDataResult<AccessToken>(Messages.TokenTimeOut);
            }

            _refreshTokenDal.Delete(_refreshToken);

            return CreateAccessToken(userControl.Data);
        }
        [ValidationAspect(typeof(UserRegisterValidator), Priority = 1)]
        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user);
        }
        [SecuredOperation]
        [ValidationAspect(typeof(UserChangePassordDtoValidator), Priority = 1)]
        public IResult ChangePassword(UserChangePassordDto userChangePassordDto)
        {
            var userControl = _userService.GetById(UserId);
            if (userControl.Data == null)
            {
                return new ErrorResult(Messages.InvalidToken);
            }

            if (!HashingHelper.VerifyPasswordHash(userChangePassordDto.OldPassword, userControl.Data.PasswordHash, userControl.Data.PasswordSalt))
            {
                return new ErrorResult(Messages.WrongPassword);
            }

            byte[] parolaHash, parolaSalt;
            HashingHelper.CreatePasswordHash(userChangePassordDto.NewPassword, out parolaHash, out parolaSalt);
            userControl.Data.PasswordHash = parolaHash;
            userControl.Data.PasswordSalt = parolaSalt;

            _userService.Update(userControl.Data);

            return new SuccessResult(Messages.UserPasswordChanged);
        }
        private void RemoveTimedOutRefreshTokens(User user)
        {
            _refreshTokenDal.DeleteRange(_refreshTokenDal.GetList(w => w.UserId == user.Id && w.ExpirationDate <= DateTime.Now).ToArray());
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Abstract;
using TaskManager.Entities.Concrete.Dtos;

namespace Takas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService { get; set; }

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var userLogin = _authService.LoginByEmail(userLoginDto);
            if (!userLogin.Success)
            {
                return BadRequest(userLogin.Message);
            }

            var result = _authService.CreateAccessToken(userLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }        
        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExist = _authService.EpostaExist(userRegisterDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }

            var registerResult = _authService.Register(userRegisterDto);

            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        [HttpPost("refresh-token/{refreshToken}")]
        public IActionResult RefreshToken(string refreshToken)
        {            
            var result = _authService.RefreshAccessToken(refreshToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePasword(UserChangePassordDto userChangePassordDto)
        {
            var result = _authService.ChangePassword(userChangePassordDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
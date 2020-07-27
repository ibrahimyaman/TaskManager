using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class UserLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

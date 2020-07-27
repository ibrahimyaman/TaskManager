using TaskManager.Core.Entities;

namespace TaskManager.Entities.Concrete.Dtos
{
    public class UserChangePassordDto:IDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

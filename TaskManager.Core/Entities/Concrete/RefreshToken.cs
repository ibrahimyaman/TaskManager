using System;

namespace TaskManager.Core.Entities.Concrete
{
    public partial class RefreshToken : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive => DateTime.Now <= ExpirationDate;
        public DateTime RegisterDate { get; set; }

        public virtual User User { get; set; }
    }
}
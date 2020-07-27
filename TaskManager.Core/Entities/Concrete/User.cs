using System;
using System.Collections.Generic;

namespace TaskManager.Core.Entities.Concrete
{
    public partial class User : IEntity
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
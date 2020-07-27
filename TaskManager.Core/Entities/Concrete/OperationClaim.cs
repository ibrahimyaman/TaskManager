using System.Collections.Generic;

namespace TaskManager.Core.Entities.Concrete
{
    public partial class OperationClaim : IEntity
    {
        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
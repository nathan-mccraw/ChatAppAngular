using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.UtcNow.AddHours(.25);
        public virtual DateTime LastActive { get; set; } = DateTime.UtcNow;
        public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
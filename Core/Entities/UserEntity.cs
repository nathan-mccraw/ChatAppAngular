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
        public virtual string Location { get; set;}
        public virtual DateTime DateOfBirth { get; set; }
        public virtual IList<UserSessionEntity> UserSessions { get; set; }

        public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public virtual DateTime? DateDeleted { get; set; } = null;
    }
}
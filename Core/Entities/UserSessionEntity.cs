using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserSessionEntity : BaseEntity
    {
        public virtual int UserId { get; set; }
        public virtual string Username { get; set; }
        public virtual DateTime LastActive { get; set; } = DateTime.UtcNow;
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.UtcNow.AddHours(.25);
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
    }
}
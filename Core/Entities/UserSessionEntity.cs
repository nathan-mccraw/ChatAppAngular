using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserSessionEntity
    {
        public virtual int SessionId { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.Now.AddHours(.25);
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
    }
}
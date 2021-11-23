using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserSessionModel
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Username { get; set; }
        public virtual DateTime LastActive { get; set; }
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.Now.AddHours(.25);
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
    }
}
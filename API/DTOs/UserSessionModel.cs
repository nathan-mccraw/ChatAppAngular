using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserSessionModel
    {
        public virtual int UserId { get; set; }
        public virtual DateTime LastActive { get; set; }
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.Now.AddHours(.25);
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
    }
}
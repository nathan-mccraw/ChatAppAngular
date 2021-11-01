using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserEntity
    {
        public virtual int UserId { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateCreated { get; set; } = DateTime.Now;
        public virtual DateTime LastActive { get; set; }
        public Guid UserToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
using System;

namespace Core.Entities
{
    public class UserSessionEntity : BaseEntity
    {
        public virtual int UserId { get; set; }
        public virtual Guid UserToken { get; set; } = Guid.NewGuid();
        public virtual DateTime TokenExpirationDate { get; set; } = DateTime.UtcNow.AddHours(.25);
        public virtual DateTime LastActive { get; set; } = DateTime.UtcNow;
        public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public virtual bool isActive => TokenExpirationDate > DateTime.Now;
    }
}
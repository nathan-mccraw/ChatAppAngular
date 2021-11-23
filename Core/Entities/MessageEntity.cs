using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MessageEntity : BaseEntity
    {
        public virtual string Text { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Username { get; set; }
        public virtual DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
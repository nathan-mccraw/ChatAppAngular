using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserSessionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid UserToken { get; set; }
    }
}
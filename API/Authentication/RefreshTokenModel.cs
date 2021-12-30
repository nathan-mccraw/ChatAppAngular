using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Authentication
{
    public class RefreshTokenModel
    {
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public Guid RefreshToken { get; set; }

    }
}

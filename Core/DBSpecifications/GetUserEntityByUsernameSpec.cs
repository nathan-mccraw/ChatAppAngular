using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class GetUserEntityByUsernameSpec : BaseSpecification<UserEntity>
    {
        public GetUserEntityByUsernameSpec(string userName)
            : base(u => u.Username.ToLower() == userName.ToLower())
        {
        }
    }
}
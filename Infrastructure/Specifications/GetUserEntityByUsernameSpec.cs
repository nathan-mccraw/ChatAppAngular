using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class GetUserEntityByUsernameSpec : BaseSpecification<UserEntity>
    {
        public GetUserEntityByUsernameSpec(string userName)
            : base(u => u.Username == userName)
        {
        }
    }
}
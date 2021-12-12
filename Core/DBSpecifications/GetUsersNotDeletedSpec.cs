using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DBSpecifications
{
    public class GetUsersNotDeletedSpec : BaseSpecification<UserEntity>
    {
        public GetUsersNotDeletedSpec() : base(u => u.DateDeleted == null)
        {
        }
    }
}
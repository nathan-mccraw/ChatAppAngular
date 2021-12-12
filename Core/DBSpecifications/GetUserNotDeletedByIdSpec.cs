using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DBSpecifications
{
    public class GetUserNotDeletedByIdSpec : BaseSpecification<UserEntity>
    {
        public GetUserNotDeletedByIdSpec(int userId) : base(u => u.Id == userId && u.DateDeleted == null)
        {
        }
    }
}
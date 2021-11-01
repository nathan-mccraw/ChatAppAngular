using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserVerification
{
    public class UserVerify
    {
        private readonly IUserRepository _userRepo;

        public UserVerify(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public bool IsValid(UserSessionEntity user)
        {
            try
            {
                _userRepo.GetUserByIdFromDB(user.UserId);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
    }
}
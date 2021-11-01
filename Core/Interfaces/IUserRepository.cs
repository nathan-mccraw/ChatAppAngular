using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsersListFromDB();

        UserModel GetUserByIdFromDB(int id);

        UserEntity GetUserEntity(int id);

        void PostUserToDB(UserEntity user);

        string DeleteUserFromDB(SignInModel userAttempt);

        UserModel CreateUser(UserEntity userAttempt);

        UserModel EditUser(UserEntity userAttempt);

        UserModel SignIn(SignInModel userAttempt);
    }
}
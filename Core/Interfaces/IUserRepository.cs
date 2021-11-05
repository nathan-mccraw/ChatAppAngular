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

        Task<UserModel> GetUserByIdFromDB(int id);

        Task<UserEntity> GetUserEntity(int id);

        void PostUserToDB(UserEntity user);

        Task<string> DeleteUserFromDB(SignInModel userAttempt);

        Task<UserModel> CreateUser(UserEntity userAttempt);

        Task<UserModel> EditUser(UserEntity userAttempt);

        Task<UserModel> SignIn(SignInModel userAttempt);
    }
}
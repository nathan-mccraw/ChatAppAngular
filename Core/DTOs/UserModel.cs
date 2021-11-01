using Core.Entities;
using System;

namespace Core.DTOs
{
    public class UserModel
    {
        public UserModel(UserEntity user)
        {
            this.UserId = user.UserId;
            this.Username = user.Username;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.DateCreated = user.DateCreated;
            this.LastActive = user.LastActive;
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public Guid UserToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActive { get; set; }
    }
}
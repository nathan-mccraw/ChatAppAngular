using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MessageModel
    {
        public MessageModel(MessageEntity message)
        {
            this.Text = message.Text;
            this.User = new UserModel(message.User);
            this.DateCreated = message.DateCreated;
        }

        public MessageModel()
        {
        }

        public string Text { get; set; }
        public UserModel User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InputValidationModels
{
    public class IncomingMessageModel
    {
        public string Text { get; set; }
        public int ChannelId { get; set; }
        public UserSessionModel UserSession { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
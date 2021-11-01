using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InputValidationModels
{
    public class IncomingMessageModel
    {
        public string Text { get; set; }
        public UserSessionEntity User { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.InputValidationModels
{
    public class IncomingSignInModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
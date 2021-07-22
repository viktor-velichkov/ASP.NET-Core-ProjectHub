using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class ValidationErrorMessages
    {
        public const string InvalidUserTypeMessage = "Please, choose a valid user type.";

        public const string UserInvalidEmailGivenMessage = "There is not a user with the given email.";
        public const string UserInvalidPasswordGivenMessage = "There is not a user with the given password.";
        public const string UserDoesNotExistMessage = "There is not a user with the given credentials.";
    }
}

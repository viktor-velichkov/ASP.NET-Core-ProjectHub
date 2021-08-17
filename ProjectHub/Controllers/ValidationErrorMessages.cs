namespace ProjectHub.Controllers
{
    public class ValidationErrorMessages
    {
        public const string UserAlreadyExists = "User with the given email already exists.";

        public const string InvalidUserKindMessage = "Please, choose a valid user kind.";

        public const string InvalidUser = "There is not any registered user with the given Id and Username.";        

        public const string InvalidDisciplineMessage = "Please, choose a valid discipline.";

        public const string UserInvalidCredentialsGivenMessage = "Invalid credentials.";

        public const string UserDoesNotExistMessage = "There is not a user with the given credentials.";

        public const string ProjectInvestorNotLoggedMessage = "Only the investor of this project has access to its offers.";


    }
}

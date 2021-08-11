namespace ProjectHub.Controllers
{
    public class ValidationErrorMessages
    {
        public const string InvalidUserKindMessage = "Please, choose a valid user kind.";

        public const string InvalidDisciplineMessage = "Please, choose a valid discipline.";

        public const string UserInvalidEmailGivenMessage = "There is not a user with the given email.";
        public const string UserInvalidPasswordGivenMessage = "There is not a user with the given password.";
        public const string UserDoesNotExistMessage = "There is not a user with the given credentials.";

        public const string ProjectInvestorNotLoggedMessage = "Only the investor of this project has access to its offers.";


    }
}

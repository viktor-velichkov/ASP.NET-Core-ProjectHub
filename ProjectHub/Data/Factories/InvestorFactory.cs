using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class InvestorFactory : UserTypeFactory
    {
        public InvestorFactory(UserType userType, int userId) : base(userType, userId)
        {
        }

        public Investor Create()
        {
            return new Investor
            {
                UserId = this.userId
            };
        }
    }
}

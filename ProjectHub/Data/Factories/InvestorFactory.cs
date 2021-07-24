using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class InvestorFactory : UserKindFactory
    {
        public InvestorFactory(UserKind userType, int userId) : base(userType, userId)
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

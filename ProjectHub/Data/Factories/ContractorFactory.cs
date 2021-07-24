using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class ContractorFactory : UserKindFactory
    {
        public ContractorFactory(UserKind userType, int userId) : base(userType, userId)
        {
        }

        public Contractor Create()
        {
            return new Contractor
            {
                UserId = this.userId
            };
        }
    }
}

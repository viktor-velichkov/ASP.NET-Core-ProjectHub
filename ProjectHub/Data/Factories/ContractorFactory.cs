using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class ContractorFactory : UserTypeFactory
    {
        public ContractorFactory(UserType userType, int userId) : base(userType, userId)
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

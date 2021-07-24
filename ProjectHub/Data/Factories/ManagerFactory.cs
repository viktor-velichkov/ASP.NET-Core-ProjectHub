using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class ManagerFactory : UserKindFactory
    {
        public ManagerFactory(UserKind userType, int userId) : base(userType, userId)
        {
        }
        public Manager Create()
        {
            return new Manager
            {
                UserId = this.userId
            };
        }

    }
}

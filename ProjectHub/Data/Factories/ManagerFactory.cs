using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class ManagerFactory : UserTypeFactory
    {
        public ManagerFactory(UserType userType, int userId) : base(userType, userId)
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

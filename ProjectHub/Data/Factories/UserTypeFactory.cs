using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public abstract class UserTypeFactory
    {
        private UserType userType;
        internal int userId;

        public UserTypeFactory(UserType userType, int userId)
        {
            this.userType = userType;
            this.userId = userId;
        }

    }
}

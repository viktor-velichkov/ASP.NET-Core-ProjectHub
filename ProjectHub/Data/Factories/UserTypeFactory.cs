using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public abstract class UserKindFactory
    {
        private UserKind userType;
        internal int userId;

        public UserKindFactory(UserKind userType, int userId)
        {
            this.userType = userType;
            this.userId = userId;
        }

    }
}

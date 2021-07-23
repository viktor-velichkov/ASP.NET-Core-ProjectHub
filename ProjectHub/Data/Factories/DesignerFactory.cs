using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class DesignerFactory : UserTypeFactory
    {
        public DesignerFactory(UserType userType, int userId) : base(userType, userId)
        {
        }

        public Designer Create()
        {
            return new Designer
            {
                UserId = this.userId
            };
        }

    }
}

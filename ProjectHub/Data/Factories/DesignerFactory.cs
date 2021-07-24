using ProjectHub.Data.Models;

namespace ProjectHub.Data.Factories
{
    public class DesignerFactory : UserKindFactory
    {
        public DesignerFactory(UserKind userType, int userId) : base(userType, userId)
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

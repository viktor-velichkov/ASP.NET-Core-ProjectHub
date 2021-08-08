using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Services.Account
{
    public interface IAccountService
    {
        public IEnumerable<UserKindRegisterFormModel> GetUserKinds();
        public IEnumerable<Discipline> GetDisciplines();
        public int GetUserKindId(string userKindName);
        public bool ConfirmThatUserKindIsValid(int userKindId);
        public bool ConfirmThatDisciplineIsValid(int disciplineId);
        public void CreateUserKindEntityRecord(int userKindId, int userId, int disciplineId);
    }
}

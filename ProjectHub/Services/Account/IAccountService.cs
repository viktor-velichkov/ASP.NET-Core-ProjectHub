namespace ProjectHub.Services.Account
{
    public interface IAccountService
    {        
        public int GetUserKindId(string userKindName);
        public bool ConfirmThatUserKindIsValid(int userKindId);
        public bool ConfirmThatDisciplineIsValid(int disciplineId);
        public void CreateUserKindEntityRecord(int userKindId, int userId, int disciplineId);
    }
}

using ProjectHub.Controllers;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using System;
using System.Linq;

namespace ProjectHub.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ProjectHubDbContext data;

        public AccountService(ProjectHubDbContext data)
        {
            this.data = data;
        }

        public bool ConfirmThatDisciplineIsValid(int disciplineId)
            => this.data.Disciplines.Any(d => d.Id.Equals(disciplineId));

        public bool ConfirmThatUserKindIsValid(int userKindId)
            => this.data
                   .UserKinds
                   .Any(d => d.Id.Equals(userKindId));


        public int GetUserKindId(string userKindName)
            => this.data.UserKinds.FirstOrDefault(uk => uk.Name.Equals(userKindName)).Id;

        public void CreateUserKindEntityRecord(int userKindId, int userId, int disciplineId)
        {
            var userKind = this.data.UserKinds.FirstOrDefault(uk => uk.Id.Equals(userKindId)).Name;

            switch (userKind)
            {
                case "Investor":
                    var investor = new Investor { Id = userId, UserId = userId };
                    this.data.Investors.Add(investor);
                    break;
                case "Designer":
                    this.data.Designers.Add(new Designer { Id = userId, UserId = userId, DisciplineId = disciplineId });
                    break;
                case "Manager":
                    this.data.Managers.Add(new Manager { Id = userId, UserId = userId });
                    break;
                case "Contractor":
                    this.data.Contractors.Add(new Contractor { Id = userId, UserId = userId });
                    break;
                default:
                    throw new ArgumentException(ValidationErrorMessages.InvalidUserKindMessage);
            }

            this.data.SaveChanges();
        }
    }
}

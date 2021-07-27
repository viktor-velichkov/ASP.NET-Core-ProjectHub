﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Project;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectHub.Services.User
{
    public class UserService : IUserService
    {
        private readonly ProjectHubDbContext data;
        private readonly IMapper mapper;

        public UserService(ProjectHubDbContext data,
                           IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public object GetUserKindEntityByUserId(string userKind, int userId)
        {
            object result = null;

            switch (userKind)
            {
                case "Investor":
                    result = this.data
                                 .Investors
                                 .Include(i => i.User)
                                 .Include(i => i.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Manager":
                    result = this.data
                                 .Managers
                                 .Include(m => m.User)
                                 .Include(m => m.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Designer":
                    result = this.data
                                 .Designers
                                 .Include(d => d.User)
                                 .Include(d => d.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Contractor":
                    result = this.data
                                 .Contractors
                                 .Include(c => c.User)
                                 .Include(c => c.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                default:
                    break;
            }

            return result;
        }

        public void EditUser(UserEditProfileViewModel model)
        {
            var userDb = this.data.Users.FirstOrDefault(u => u.Id.Equals(model.User.Id));

            userDb.Image = model.User.Image;
            userDb.FirstName = model.User.FirstName;
            userDb.LastName = model.User.LastName;
            userDb.Email = model.User.Email;
            userDb.PhoneNumber = model.User.PhoneNumber;
            userDb.WebSite = model.User.WebSite;
            userDb.Description = model.User.Description;
            userDb.FacebookPage = model.User.FacebookPage;
            userDb.LinkedinPage = model.User.LinkedinPage;
            userDb.SkypeProfile = model.User.SkypeProfile;

            UpdateUserKindEntityModel(model, userDb);

            this.data.SaveChanges();
        }

        public IEnumerable<ProjectGeneralViewModel> GetUserProjects(int id, string userKind)
        {
            List<ProjectGeneralViewModel> projects = new();
            switch (userKind)
            {
                case "Investor":
                    var investorProjects = this.data
                                               .Investors
                                               .FirstOrDefault(i => i.UserId.Equals(id))
                                               .Projects
                                               .Select(p => p.Project)
                                               .ToList();

                    projects = this.mapper.Map<List<Project>, List<ProjectGeneralViewModel>>(investorProjects);
                    break;
                case "Manager":
                    var managerProjects = this.data
                                               .Managers
                                               .FirstOrDefault(i => i.UserId.Equals(id))
                                               .Projects
                                               .ToList();

                    projects = this.mapper.Map<List<Project>, List<ProjectGeneralViewModel>>(managerProjects);
                    break;
                case "Designer":
                    var designerProjects = this.data
                                               .Designers
                                               .FirstOrDefault(i => i.UserId.Equals(id))
                                               .Projects
                                               .ToList();

                    projects = this.mapper.Map<List<ProjectDesigner>, List<ProjectGeneralViewModel>>(designerProjects);
                    break;
                case "Contractor":
                    var contractorProjects = this.data
                                                 .Contractors
                                                 .FirstOrDefault(i => i.UserId.Equals(id))
                                                 .Projects
                                                 .ToList();

                    projects = this.mapper.Map<List<ProjectContractor>, List<ProjectGeneralViewModel>>(contractorProjects);
                    break;
                default:
                    break;
            }

            return projects;
        }

        private void UpdateUserKindEntityModel(UserEditProfileViewModel model, ApplicationUser user)
        {
            switch (model.User.UserKindName)
            {
                case "Investor":
                    var investor = this.data.Investors.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    investor.User = user;
                    break;
                case "Manager":
                    var manager = this.data.Managers.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    manager.User = user;
                    break;
                case "Designer":
                    var designer = this.data.Designers.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    designer.User = user;
                    designer.Discipline = model.Discipline;
                    designer.WorkExperience = model.WorkExperience;
                    break;
                case "Contractor":
                    var contractor = this.data.Contractors.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    contractor.User = user;
                    contractor.Activities = model.Activities;
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<ReviewViewModel> GetUserReviews(int id)
        {
            var userReviews = this.data
                                  .Users
                                  .SelectMany(u => u.ReviewsReceived)
                                  .Where(r => r.RecipientId.Equals(id))
                                  .Select(r => this.mapper.Map<Review, ReviewViewModel>(r))
                                  .ToList();

            return userReviews;
        }

        public IEnumerable<DiscussionViewModel> GetUserDiscussions(int id)
        {
            var userDiscussions = this.data
                                      .UserDiscussions
                                      .Where(ud => ud.UserId.Equals(id))
                                      .Select(ud => ud.Discussion)
                                      .Select(d => this.mapper.Map<Discussion, DiscussionViewModel>(d))
                                      .ToList();
            return userDiscussions;
        }

        public byte[] GetUserImage(int id)
            => this.data.Users.FirstOrDefault(u => u.Id.Equals(id)).Image;

        public byte[] ProcessUploadedFile(IFormFile file)
        {
            byte[] result = null;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);



                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    result = memoryStream.ToArray();
                }
            }

            return result;
        }
    }
}

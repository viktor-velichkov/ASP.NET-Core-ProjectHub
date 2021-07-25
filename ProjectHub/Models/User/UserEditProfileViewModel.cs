﻿using ProjectHub.Data.Models;
using ProjectHub.Models.Project;
using System.Collections.Generic;

namespace ProjectHub.Models.User
{
    public class UserEditProfileViewModel
    {
        public bool IsLoggedUser { get; set; }
        public AppUserEditProfileViewModel User { get; set; }
        public string UserKindName { get; set; }

        public Discipline Discipline { get; set; }

        public int? WorkExperience { get; set; }

        public List<Activity> Activities { get; set; }
    }
}

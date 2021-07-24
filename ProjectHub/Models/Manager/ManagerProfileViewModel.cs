﻿using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Manager
{
    public class ManagerProfileViewModel
    {
        public AppUserProfileViewModel User { get; set; }

        public ICollection<ProjectGeneralViewModel> Projects { get; set; }
    }
}
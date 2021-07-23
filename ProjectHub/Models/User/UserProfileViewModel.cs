﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Models.User
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }

        public string UserType { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Recomendations { get; set; }

        public int Disapprovals { get; set; }

        
    }
}

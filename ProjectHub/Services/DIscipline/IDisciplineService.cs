﻿using ProjectHub.Data.Models;
using System.Collections.Generic;

namespace ProjectHub.Services.DIscipline
{
    public interface IDisciplineService
    {
        public ICollection<Discipline> GetAllDisciplines();
    }
}

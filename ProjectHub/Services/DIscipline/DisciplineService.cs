﻿using ProjectHub.Data;
using ProjectHub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectHub.Services.DIscipline
{
    public class DisciplineService : IDisciplineService
    {
        private readonly ProjectHubDbContext data;

        public DisciplineService(ProjectHubDbContext data)
        {
            this.data = data;
        }

        public ICollection<Discipline> GetAllDisciplines()
            => this.data.Disciplines.ToList();
    }
}

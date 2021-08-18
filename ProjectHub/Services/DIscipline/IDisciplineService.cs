using ProjectHub.Data.Models;
using System.Collections.Generic;

namespace ProjectHub.Services.Disciplines
{
    public interface IDisciplineService
    {
        public List<Discipline> GetAllDisciplines();
    }
}

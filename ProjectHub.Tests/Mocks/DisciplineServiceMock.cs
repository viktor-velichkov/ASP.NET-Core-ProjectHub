using Moq;
using ProjectHub.Data.Models;
using ProjectHub.Services.Disciplines;
using System.Collections.Generic;

namespace ProjectHub.Tests.Mocks
{
    public static class DisciplineServiceMock
    {
        public static IDisciplineService Instance
        {
            get
            {
                var disciplineService = new Mock<IDisciplineService>();

                disciplineService
                    .Setup(ds => ds.GetAllDisciplines())
                    .Returns(new List<Discipline>() { new Discipline { Name = "Designer" } });

                return disciplineService.Object;
            }
        }
    }
}

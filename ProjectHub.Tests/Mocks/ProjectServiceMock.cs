using Moq;
using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using ProjectHub.Services.Projects;
using System.Collections.Generic;
using System.Linq;

namespace ProjectHub.Tests.Mocks
{
    public static class ProjectServiceMock
    {
        public static IProjectService Instance
        {
            get
            {
                var projectService = new Mock<IProjectService>();

                projectService
                    .Setup(ps => ps.ProjectExists(1))
                    .Returns(true);

                projectService
                    .Setup(ps => ps.ProjectExists(2))
                    .Returns(true);

                projectService
                    .Setup(ps => ps.ProjectExists(-1))
                    .Returns(false);

                projectService
                    .Setup(ps => ps.GetAllProjectsOrderedByDateDescending())
                    .Returns(Enumerable.Range(0, 10).Select(x => new ProjectCardViewModel { }).ToList());

                projectService
                    .Setup(ps => ps.GetAllProjectCities())
                    .Returns(Enumerable.Range(0, 5).Select(x => x.ToString()).ToList());

                projectService
                    .Setup(ps => ps.FilterByCity("Sofia"))
                    .Returns(Enumerable.Range(0, 5).Select(x => new ProjectCardViewModel { City = "Sofia" }).ToList());

                projectService
                    .Setup(ps => ps.GetProjectDetailsViewModel(1))
                    .Returns(new ProjectDetailsViewModel
                    {
                        Designers = new List<DesignerProjectDetailsViewModel> {
                            new DesignerProjectDetailsViewModel { Discipline = "Architecture" } }
                    });

                projectService
                    .Setup(ps => ps.GetProjectDesignersByProjectId(1))
                    .Returns(new List<DesignerProjectDetailsViewModel>()
                    {
                        new DesignerProjectDetailsViewModel { Discipline= "Architecture" }
                    });

                projectService
                    .Setup(ps => ps.GetDesignerDisciplineName(3))
                    .Returns("Designer");

                projectService
                    .Setup(ps => ps.GetProjectWithOffersById(1))
                    .Returns(new ProjectOffersListViewModel
                    {
                        InvestorId = 3,
                        Disciplines = new List<Discipline>() { new Discipline { } }
                    });

                projectService
                    .Setup(ps => ps.IsOwnerOfTheProject(4, 1))
                    .Returns(false);

                projectService
                    .Setup(ps => ps.IsOwnerOfTheProject(4, 2))
                    .Returns(true);

                return projectService.Object;
            }
        }
    }
}

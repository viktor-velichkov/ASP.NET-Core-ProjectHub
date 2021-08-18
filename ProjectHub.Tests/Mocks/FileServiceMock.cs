using Microsoft.AspNetCore.Http;
using Moq;
using ProjectHub.Services.Files;

namespace ProjectHub.Tests.Mocks
{
    public static class FileServiceMock
    {
        public static IFilesService Instance
        {
            get
            {
                var filesService = new Mock<FilesService>();

                filesService
                    .Setup(fs => fs.ProcessUploadedFile(Mock.Of<FormFile>()))
                    .Returns(new byte[] { });

                return filesService.Object;
            }
        }
    }
}

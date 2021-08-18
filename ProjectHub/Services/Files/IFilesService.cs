using Microsoft.AspNetCore.Http;

namespace ProjectHub.Services.Files
{
    public interface IFilesService
    {
        public byte[] ProcessUploadedFile(IFormFile file);
    }
}

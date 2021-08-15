using Microsoft.AspNetCore.Http;
using System.IO;

namespace ProjectHub.Services.Files
{
    public class FilesService : IFilesService
    {
        public byte[] ProcessUploadedFile(IFormFile file)
        {
            byte[] result = null;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    result = memoryStream.ToArray();
                }
            }

            return result;
        }
    }
}

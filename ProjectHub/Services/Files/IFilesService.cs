using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Services.Files
{
    public interface IFilesService
    {
        public byte[] ProcessUploadedFile(IFormFile file);
    }
}

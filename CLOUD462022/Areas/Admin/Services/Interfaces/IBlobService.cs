using CLOUD462022.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Areas.Admin.Services.Interfaces
{
    public interface IBlobService
    {
        Task<string> GetBlob(string name, string containerName);
        Task<List<string>> GetAllBlobs(string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName, Blob blob);
        Task<bool> DeleteBlob(string name, string containerName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Areas.Admin.Services.Interfaces
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}

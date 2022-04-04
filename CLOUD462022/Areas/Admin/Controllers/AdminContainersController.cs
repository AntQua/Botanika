using CLOUD462022.Areas.Admin.Services.Interfaces;
using CLOUD462022.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminContainersController : Controller
    {
        private readonly IContainerService _containerService;
        public AdminContainersController(IContainerService containerService)
        {
            _containerService = containerService;
        }
        public async Task<IActionResult> Index()
        {
            var allContainers = await _containerService.GetAllContainers();
            return View(allContainers);
        }

        public async Task<IActionResult> Delete(string containerName)
        {
            await _containerService.DeleteContainer(containerName);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View(new Container());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await _containerService.CreateContainer(container.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}

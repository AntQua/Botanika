using CLOUD462022.Services.Interfaces;
using CLOUD462022.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBlobsController : Controller
    {
        private readonly IBlobService _blobService;
        public AdminBlobsController(IBlobService blobService)
        {
            _blobService = blobService;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containerName )
        {
            var blobsObj = await _blobService.GetAllBlobs(containerName);

            return View(blobsObj);
        }

        [HttpGet]
        public IActionResult AddFile(string containerName)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(string containerName, Blob blob, IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            var fileName = Path.GetFileName(file.FileName) /*+ "_" + Guid.NewGuid() + Path.GetExtension(file.FileName)*/;
            var result = await _blobService.UploadBlob(fileName, file, containerName, blob);

            if (result)
                return RedirectToAction("Index", "AdminContainers");

            return View();
        }

        public async Task<IActionResult> ViewFile(string name, string containerName)
        {
            return Redirect(await _blobService.GetBlob(name, containerName));
        }

        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobService.DeleteBlob(name, containerName);

            return RedirectToAction("Index", "AdminContainers");
        }
    }
}

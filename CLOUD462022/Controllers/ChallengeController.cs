using CLOUD462022.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IContainerService _containerService;
        private readonly IBlobService _blobService;

        public ChallengeController(IContainerService containerService, IBlobService blobService)
        {
            _containerService = containerService;
            _blobService = blobService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blobService.GetBlobsWithUri("imageschallenge"));
        }


    }
}

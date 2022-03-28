using CLOUD462022.Repositories.Interfaces;
using CLOUD462022.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLOUD462022.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                FavouriteProducts = _productRepository.FavouritProducts
            };

            return View(homeViewModel);
        }

        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}

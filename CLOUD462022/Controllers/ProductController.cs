using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using CLOUD462022.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository { get; }

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List(string category)
        {
            IEnumerable<Product> products;
            string categoryProd = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
                categoryProd = "All Products";
            }
            else
            {
                products = _productRepository.Products
                           .Where(p => p.Category.CategoryName.Equals(category))
                           .OrderBy(p => p.ProductName);

                categoryProd = category;
            }

            var productListViewModel = new ProductListViewModel
            {
                Products = products,
                CategoryProd = categoryProd
            };

            return View(productListViewModel);

        }

        public ViewResult Details(int productId)
        {
            var product = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(product);
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Product> products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
            }
            else
            {
                products = _productRepository.Products.Where(p => p.ProductName.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Product/List.cshtml", new ProductListViewModel
            {
                Products = products,
                CategoryProd = "All products"
            });
        }
    }
}

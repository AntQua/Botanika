using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLOUD462022.Repositories;
using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;

namespace CLOUD462022.Tests.FakeRepositories
{
    internal class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products => new List<Product>()
            {
                new Product { ProductId=1, ProductName="Rose", Price=20,ImageUrl ="xxx", ImageThumbnailUrl= "xxx", Description="Product description", InStock=true, IsFavourite=true, CategoryId=1/*, Category={CategoryId=1, CategoryName="Flowers"} */},
                new Product { ProductId=2, ProductName="Monstera", Price=30,ImageUrl ="xxx", ImageThumbnailUrl= "xxx", Description="Product description", InStock=true, IsFavourite=true, CategoryId=2/*, Category={CategoryId=2, CategoryName="Plants"} */},
                new Product { ProductId=3, ProductName="Gardening tools", Price=50,ImageUrl ="xxx", ImageThumbnailUrl= "xxx", Description="Product description", InStock=true, IsFavourite=true, CategoryId=3/*, Category={CategoryId=3, CategoryName="Gardening"} */}

            };


        public IEnumerable<Product> FavouritProducts => throw new NotImplementedException();

        public Product GetProductById(int productId)
        {
            return new Product();
        }
    }

}

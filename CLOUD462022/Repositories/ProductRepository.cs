using CLOUD462022.Context;
using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CLOUD462022.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CLOUD462022DbContext _context;

        public ProductRepository(CLOUD462022DbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products => _context.Products.Include(c => c.Category);

        public IEnumerable<Product> FavouritProducts => _context.Products.
                                    Where(p => p.IsFavourite).
                                    Include(c => c.Category);

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}

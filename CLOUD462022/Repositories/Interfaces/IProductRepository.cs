using CLOUD462022.Models;
using System.Collections.Generic;

namespace CLOUD462022.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> FavouritProducts { get; }
        Product GetProductById(int productId);

    }
}

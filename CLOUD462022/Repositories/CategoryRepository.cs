using CLOUD462022.Context;
using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using System.Collections.Generic;

namespace CLOUD462022.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CLOUD462022DbContext _context;
        public CategoryRepository(CLOUD462022DbContext context)
        {
            _context = context; 
        }

        IEnumerable<Category> ICategoryRepository.Categories => _context.Categories;
    }
}

using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLOUD462022.Tests.FakeRepositories
{
    internal class FakeCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories => new List<Category>()
            {
                new Category { CategoryId=1, CategoryName="Flowers"},
                new Category { CategoryId=2, CategoryName="Plants"},
                new Category { CategoryId=3, CategoryName="Gardening"}

            };
    }
}

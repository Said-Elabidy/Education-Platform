using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<Categories>, ICategoryRepository
    {

        public CategoryRepository(EducationPlatformDBContext context) : base(context)
        {

        }
    

    }
}

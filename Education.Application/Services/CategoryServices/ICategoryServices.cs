using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Categories>> GetCategories();

        Task<Categories> GetCategoryById(int id);

        Task<bool> Update(Categories Category);

        Task<bool> Delete(int id);

        Task Add(Categories Category);
        Task<IEnumerable<Categories>> SearchCategoryByName(string name);
    }


}


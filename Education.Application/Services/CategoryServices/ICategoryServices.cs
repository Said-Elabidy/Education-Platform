﻿using Education.Domain.Entities;


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


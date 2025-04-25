using Education.Domain.Entities;


namespace Education.Application.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Categories>> GetCategories();

        Task<Categories?> GetCategoryById(int id);

        Task<Categories?> GetCategoryCourseById(int courseId);

        Task<bool> Update(Categories Category);
        public Task<bool> Update(int id, Categories Category);

        Task<bool> Delete(int id);

        Task<bool> Add(Categories Category);
        Task<IEnumerable<Categories>> SearchCategoryByName(string name);
    }


}


using Education.Domain.Entities;
using Education.Domain.Repository;

namespace Education.Application.Services.CategoryServices
{
    public class CategoryService : ICategoryServices
    {
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        private readonly ICategoryRepository _categoryRepository;

        public async Task<bool> Add(Categories Category)
        {
            await _categoryRepository.AddAsync(Category);
           return await _categoryRepository.SaveChangesAsync();
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var res = await _categoryRepository.Delete(id);
                if (res) { 
             var result = await _categoryRepository.SaveChangesAsync();
                     if(result)
                        return true;
                }
                return false;
               
            }
            catch
            {
                return false;
            }

        }

        public async Task<Categories?> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetNotDeletedCategoryById(id);
            //if (category == null)
            //{
            //    throw new KeyNotFoundException($"Category with ID {id} not found.");
            //}
            return category;
        }
        public  Task<IEnumerable<Categories>> GetCategories()
        {
            var Categories =  _categoryRepository.GetAllAsync();
            return  Categories;

        }
        public async Task<bool> Update(Categories Category)
        {
            try
            {
                _categoryRepository.Update(Category);
              var res =  await _categoryRepository.SaveChangesAsync();
                if(res)
                return true;
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> Update(int id,Categories Category)
        {
            try
            {
               var category = await _categoryRepository.GetNotDeletedCategoryById(id);
                if (category != null)
                {
                    category.LastUpdateOn = Category.LastUpdateOn;
                    category.Name = Category.Name;
                }
                else
                {
                    return false;
                }
                    var res = await _categoryRepository.SaveChangesAsync();
                if (res)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }

        }
        public async Task<IEnumerable<Categories>> SearchCategoryByName(string name)
        {
            var Categories = await _categoryRepository.GetAllAsync();
            var result = Categories.Where(c => c.Name.Contains(name));
            return result;
        }

        public async Task<Categories?> GetCategoryCourseById(int courseId)
        {
            var category = await _categoryRepository.GetCategoryCourseById(courseId);
            //if (category == null)
            //{
            //    throw new KeyNotFoundException($"Category In This Course ID {courseId} not found.");
            //}
            return category;
        }
    }
    
}

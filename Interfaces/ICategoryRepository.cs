using AgainPBL3.Models;

namespace AgainPBL3.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetListCategory();
        Task<Category> AddCategory(Category category);
        Task<Category> GetCategoryById(int ID);
        Task<Category?> UpdateCategory(int ID, string categoryName);
        Task<int?> DeleteCategory(int ID);
    }
}

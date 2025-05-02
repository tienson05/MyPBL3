using AgainPBL3.Dtos.Category;
using AgainPBL3.Models;

namespace AgainPBL3.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto MapToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                CategoryName = category.CategoryName
            };
        }
    }
}

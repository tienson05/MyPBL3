using System.Runtime.CompilerServices;
using AgainPBL3.Dtos.Product;
using AgainPBL3.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace AgainPBL3.Mappers
{
    public static class ProductMappers
    {
        public static Product MapToProduct(this CreateProductDto createProductDto, int UserID)
        {
            return new Product
            {
                UserID = UserID,
                CategoryID = createProductDto.CategoryID,
                Title = createProductDto.Title,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
                Condition = createProductDto.Condition,
                Images = createProductDto.Images,
                Location = createProductDto.Location,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
        }
    }
}

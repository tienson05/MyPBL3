using AgainPBL3.Models;

namespace AgainPBL3.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetListProduct(
            int? categoryId,
            int? userId,
            string status,
            string keyword,
            int pageNumber,
            int pageSize
        );
        Task<Product> GetProductByID(int ID);
        Task<Product> AddProduct(Product product);
        Task<int?> DeleteProduct(int ID);
        Task<Product?> ApprovedProduct(int id);
        Task<Product?> UpdateProduct(
            int ID,
            string? Title,
            string? Description,
            double? Price,
            int? CategoryID,
            string? Condition,
            string? Images,
            string? Location
        );
    }
}

using AgainPBL3.Models;

namespace AgainPBL3.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByID(int id);
        Task<PagedResult<Order>> GetListOrder(int? BuyerId, int? VendorID, Decimal? TotalPrice, string? Status, int? OrderDetailID, int? ProductID,string? NameProduct, int pageNumber, int pageSize);
        Task<Order?> GetDetailOrderByID(int orderID);
        Task<Order> AddOrder(Order order);
        Task<int?> DeleteOrder(int id);
        Task<Order?> UpdateOrder(int OrderID, int BuyerId, int VendorID, Decimal TotalPrice, string Status,int OrderDetailID, int ProductID,int price);
    }
}

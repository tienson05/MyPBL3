using AgainPBL3.Dtos.Order;
using AgainPBL3.Models;

namespace AgainPBL3.Mappers
{
    public static class OrderMappers
    {
        public static Order OrderDtoToOrder(this OrderDto dto)
        {
            return new Order
            {
                BuyerId = dto.BuyerID,
                VendorId = dto.VendorID,
                TotalPrice = dto.TotalPrice,
                CreatedAt = DateTime.UtcNow,
                OrderDetails = dto.OrderDetails?.Select(od => new OrderDetail
                {
                    ProductID = od.ProductID,
                    Price = od.Price
                }).ToList()
            };
        }
    }
}

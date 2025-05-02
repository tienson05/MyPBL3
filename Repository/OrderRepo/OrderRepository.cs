//using Emby.Media.Model.GraphModel;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AgainPBL3.Data;

namespace AgainPBL3.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<int?> DeleteOrder(int id)
        {
            var delOrder = await _context.Orders.FindAsync(id);
            if (delOrder == null) return null;
            _context.Orders.Remove(delOrder);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Order?> GetDetailOrderByID(int orderID)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Buyer)
                .Include(o=> o.Vendor)
                .FirstOrDefaultAsync(o=> o.OrderID == orderID);
        }

        public async Task<PagedResult<Order>> GetListOrder(
            int? BuyerId,
            int? VendorID,
            decimal? TotalPrice,
            string? Status,
            int? OrderDetailID,
            int? ProductID,
            string? NameProduct,
            int pageNumber,
            int pageSize)
        {
            var querry = _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .Include(o => o.Buyer)
                .Include(o => o.Vendor)
                .AsQueryable();
            if (TotalPrice != null)
            {
                querry = querry.Where(o => o.TotalPrice == TotalPrice);
            }
            if (BuyerId != null)
            {
                querry = querry.Where(o => o.BuyerId == BuyerId);
            }
            if (VendorID != null)
            {
                querry = querry.Where(o=>o.VendorId == VendorID);
            }
            if (Status != null)
            {
                querry= querry.Where(o=>o.Status == Status);
            }
            if (NameProduct != null) {
                querry = querry.Where(o=>o.OrderDetails.Any(od => od.Product.Title.Contains(NameProduct)));
                    }
            var totalCount = await querry.CountAsync();

            var data = await querry
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Order>
            {
                Data = data,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        //public async Task<PagedResult<Order>> GetListOrderByUserID(int id, int pageNumber, int pageSize)
        //{
        //    var query = _context.Orders.Include(o => o.Buyer)
        //        .Include(o => o.Vendor)
        //        .Include(o => o.OrderDetails)
        //        .ThenInclude(o => o.Product)
        //        .Where(o => o.BuyerId == id || o.VendorID == id);
        //    var totalCount = await query.CountAsync();

        //    var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        //    return new PagedResult<Order>
        //    {
        //        Data = data,
        //        TotalItems = totalCount,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize
        //    };
        //}

        public async Task<Order?> GetOrderByID(int id)
        {
            return await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Vendor)
                .Include (o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<Order?> UpdateOrder(int  OrderID, int BuyerId, int VendorID, decimal TotalPrice, string Status,int OrderDetailID, int ProductID,int price )
        {
            var orderUpdate = await _context.Orders.Include(o=> o.OrderDetails).FirstOrDefaultAsync(o=>o.OrderID==OrderID);
            if (orderUpdate == null)
            {
                return null;
            }
            orderUpdate.BuyerId = BuyerId;
            orderUpdate.VendorId = VendorID;
            orderUpdate.TotalPrice = TotalPrice;
            orderUpdate.Status = Status;
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(o=>o.OrderDetailID ==OrderDetailID);
            if (orderDetail == null)
            {
                return null;
            }
            orderDetail.ProductID = OrderID;
            orderDetail.Price = price;
            await _context.SaveChangesAsync();
            return orderUpdate;

        }
    }
}

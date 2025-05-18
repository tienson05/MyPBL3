using AgainPBL3.Dtos.Order;
using AgainPBL3.Interfaces;
using AgainPBL3.Mappers;
using AgainPBL3.Models;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Authorization;

//using MediaBrowser.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Controllers.Clients
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AccountService _accountService;

        public OrderController(IOrderRepository orderRepository, AccountService accountService)
        {
            _orderRepository = orderRepository;
            _accountService = accountService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderByID(int id)
        {
            try
            {
                return await _orderRepository.GetOrderByID(id);
            }
            catch (Exception ex)
            {
                return NotFound("Order not found!");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetListOrder(
            [FromQuery] int? BuyerId=null,
            [FromQuery] int? VendorID=null,
            [FromQuery] decimal? TotalPrice=null,
            [FromQuery] string? Status = null,
            [FromQuery] int? OrderDetailID = null,
            [FromQuery] int? ProductID = null,
            [FromQuery] string? NameProduct = null,
            [FromQuery] string? PayMethod = null,
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize= 10)
        {
            var result = await _orderRepository.GetListOrder(BuyerId,VendorID,TotalPrice,Status,OrderDetailID,ProductID,NameProduct,PayMethod, pageNumber,pageSize);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var delID = await _orderRepository.DeleteOrder(id);
            if (delID != null) return Ok("Order delete success!");
            return NotFound("Order not found!");
        }
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] OrderDto dto)
        {
            var order = dto.OrderDtoToOrder();
            await _orderRepository.AddOrder(order);
            var savedOrder = await _orderRepository.GetDetailOrderByID(order.OrderID);
            return CreatedAtAction(nameof(GetOrderByID), new { id = order.OrderID }, order);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromQuery] String id, [FromBody] OrderUpdateDto dto)
        {
            int temp = Convert.ToInt32(id);
            var UpdateOrder = await _orderRepository.UpdateOrder(temp, dto.BuyerId, dto.VendorID, dto.TotalPrice, dto.Status, dto.OrderDetailID, dto.ProductID, dto.Price,dto.CancelReason,dto.PayMethod,dto.Quantity);
            if (UpdateOrder == null)
                return NotFound();
            return Ok(new { status = "success", message = "Canceled successfully" });
        }

    }
}

using AgainPBL3.Dtos.Cart;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using AgainPBL3.Repository.OrderRepo;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Claims;

namespace AgainPBL3.Controllers.Clients
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("id")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            try
            {
                return await _cartRepository.GetCartByID(id);
            }
            catch (Exception ex)
            {
                return NotFound("Cart Not Found!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartByUserId()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                return  Ok(await _cartRepository.GetCartByUserID(Convert.ToInt32(userId)));
                
            }
            catch (Exception ex)
            {
                return NotFound("Cart Not Found!");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCartItemToCart([FromBody] List<AddCartItemDto> dtos)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var dto in dtos)
            {
                try
                {
                    var cartItem = await _cartRepository.AddCartItemtToCart(Convert.ToInt32(userId), dto.ProductId, dto.Quantity);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
            return Ok();
        }
        [HttpPut("update/{cartItemId}")]
        public async Task<ActionResult<CartItem>> UpdateCartItem ( int cartItemId, [FromBody] UpdateCartDto dto)
        {
            try
            {
                var cart = await _cartRepository.UpdateCartItemQuantity(cartItemId, dto.Quantity);
                if (cart == null)
                {
                    return NotFound("CartItem Not found");
                }
                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
        [HttpDelete("cartItemId")]
        public async Task<ActionResult> RemoveCartItem(int cartItemId)
        {
            try
            {
                var cart = await _cartRepository.RemoveItemFromCart(cartItemId);
                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    } 
}

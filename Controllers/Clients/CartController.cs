using AgainPBL3.Dtos.Cart;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using AgainPBL3.Repository.OrderRepo;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace AgainPBL3.Controllers.Clients
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly AccountService _accountService;
        public CartController(ICartRepository cartRepository, AccountService accountService)
        {
            _cartRepository = cartRepository;
            _accountService = accountService;
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

        [HttpGet("userId")]
        public async Task<ActionResult<Cart>> GetCartByUserId(int userId)
        {
            try
            {
                return await _cartRepository.GetCartByUserID(userId);
            }
            catch (Exception ex)
            {
                return NotFound("Cart Not Found!");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCartItemToCart([FromBody] AddCartItemDto dto)
        {
            try
            {
                var cartItem = await _cartRepository.AddCartItemtToCart(dto.userId, dto.productId, dto.quantity);
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
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

using AgainPBL3.Data;
using AgainPBL3.Dtos.Cart;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Repository.CartRepo
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<Cart> AddCart(Cart c)
        //{
        //    _context.Carts.Add(c);
        //    await _context.SaveChangesAsync();
        //    return c;
        //}

        public async Task<Cart> AddCartItemtToCart(int userId, int productId, int quantity)
        {
            var user = await _context.Users.FindAsync(userId);
            var product = await _context.Products.FindAsync(productId);
            if (user == null || product == null)
            {
                throw new ArgumentException("User or product not found.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.");
            }
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null)
            {
                cart = new Cart { UserID = userId, CartItems = new List<CartItem>() };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductID == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductID = productId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
            return await GetCartByID(cart.Id);
        }

        public async Task<Cart> GetCartByID(int Id)
        {
            return await _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Cart> GetCartByUserID(int userId)
        {
            return await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId);
        }

        public async Task<Cart?> RemoveCart(int cartId)
        {
            var c= await _context.Carts.FindAsync(cartId);
            if (c != null) { 
                _context.Carts.Remove(c);
            }
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<CartItem?> RemoveItemFromCart(int cartItemId)
        {
            var ci = await _context.CartItems.Include(ci=> ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
            if (ci != null) { 
                _context.CartItems.Remove(ci);
            }
            await _context.SaveChangesAsync();
            return ci;
        }

        public async Task<CartItem> UpdateCartItemQuantity(int CartItemId, int quantity)
        {
            var ci = await _context.CartItems.Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == CartItemId);
            if (ci != null) { 
                ci.Quantity = quantity;
            }
            await _context.SaveChangesAsync();
            return ci;
        }
    }
}

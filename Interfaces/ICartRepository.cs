using AgainPBL3.Models;

namespace AgainPBL3.Interfaces
{
    public interface ICartRepository
    {
        //Task<Cart> AddCart(Cart c);
        Task<Cart> AddCartItemtToCart(int userId,int productId, int quantity);
        Task<CartItem> UpdateCartItemQuantity(int CartItemId, int quantity);
        Task<Cart?> RemoveCart(int cartItemId);
        Task<Cart> GetCartByID(int Id);
        Task<Cart> GetCartByUserID(int userId);
        Task<CartItem?> RemoveItemFromCart(int cartItemId);

    }
}

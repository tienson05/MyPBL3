using AgainPBL3.Models;

namespace AgainPBL3.Interfaces
{
    public interface ICartRepository
    {
        //Task<Cart> AddCart(Cart c);
        Task<Cart> AddCartItemtToCart(int userId,int productId, int quantity);
        Task<CartItem> UpdateCartItemQuantity(int CartItemId, int quantity);
        Task<Cart?> RemoveCart(int cartId);
        Task<Cart> GetCartByID(int Id);
        Task<int> GetCartIdByUserId(int userId);
        Task<List<Product>> GetCartByUserID(int userId);
        Task<CartItem?> RemoveItemFromCart(int cartItemId);

    }
}

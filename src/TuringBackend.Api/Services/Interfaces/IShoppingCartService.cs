using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IShoppingCartService
    {
        Task<List<CartWithProduct>> GetShoppingCartByIdAsync(string cartId);
        Task AddShoppingCartItemAsync(string cartId, int productId, string attributes);
        Task UpdateCartItemAsync(int itemId, int quantity);
        Task<string> GetCartIdByItemAsync(int item_id);
        Task EmptyCartAsync(string cartId);
        Task<decimal> GetCartTotalAmountAsync(string cartId);
        Task ShoppingSaveForLaterAsync(int itemId);
        Task MoveItemToCartAsync(int itemId);
        Task<List<ShoppingCart>> GetSavedCartItemsAsync(string cartId);
        Task RemoveItemAsync(int itemId);
    }
}
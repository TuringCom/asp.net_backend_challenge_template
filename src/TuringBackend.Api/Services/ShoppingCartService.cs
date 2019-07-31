using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly TuringBackendContext _dbContext;

        public ShoppingCartService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartWithProduct>> GetShoppingCartByIdAsync(string cartId)
        {
            var filteredCart = _dbContext
                .ShoppingCart
                .Where(i => i.CartId == cartId && i.BuyNow == 1)
                .ToList();

            var cart = from c in filteredCart
                join p in _dbContext.Product on c.ProductId equals p.ProductId
                group new {c, p} by p.ProductId
                into grouping
                select new CartWithProduct
                {
                    ItemId = grouping.First().c.ItemId,
                    ProductId = grouping.Key,
                    Attributes = grouping.First().c.Attributes,
                    Quantity = grouping.First().c.Quantity,
                    Name = grouping.First().p.Name,
                    Price = (grouping.First().p.DiscountedPrice > 0 ? grouping.First().p.DiscountedPrice : grouping.First().p.Price ).ToString(CultureInfo.InvariantCulture),
                    SubTotal = (grouping.First().c.Quantity * (grouping.First().p.DiscountedPrice > 0 ? grouping.First().p.DiscountedPrice : grouping.First().p.Price))
                        .ToString(CultureInfo.InvariantCulture)
                };
            
            var summary = await Task.FromResult(cart.ToList());
            return summary;
        }

        public async Task AddShoppingCartItemAsync(string cartId, int productId, string attributes)
        {
            // Check if we have a product like this and increment the quantity
            var item = _dbContext
                .ShoppingCart
                .FirstOrDefault(c => c.ProductId == productId && c.CartId == cartId);

            if (item != null)
            {
                await UpdateCartItemAsync(item.ItemId, item.Quantity + 1);
            }
            else
            {
                var newCartItem = new ShoppingCart
                {
                    CartId = cartId,
                    ProductId = productId,
                    Attributes = attributes,
                    AddedOn = DateTime.Now,
                    Quantity = 1,
                    BuyNow = 1
                };

                _dbContext.ShoppingCart.Add(newCartItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(int itemId, int quantity)
        {
            var item = _dbContext
                .ShoppingCart
                .First(c => c.ItemId == itemId);

            item.Quantity = quantity;
            item.AddedOn = DateTime.Now;

            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetCartIdByItemAsync(int item_id)
        {
            var cart = await _dbContext
                .ShoppingCart
                .FirstOrDefaultAsync(c => c.ItemId == item_id);
            return cart?.CartId;
        }

        public async Task EmptyCartAsync(string cartId)
        {
            var cartItems = _dbContext
                .ShoppingCart
                .Where(c => c.CartId == cartId && c.BuyNow == 1);
            foreach (var item in cartItems) 
                _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAmountAsync(string cartId)
        {
            var cartWithProducts = from pc in _dbContext.ShoppingCart
                join c in _dbContext.Product on pc.ProductId equals c.ProductId
                where pc.CartId == cartId
                select new
                {
                    pc.Quantity,
                    c.Price,
                    c.DiscountedPrice
                };

            var total = await cartWithProducts.SumAsync(c => c.Quantity * (c.DiscountedPrice > 0 ? c.DiscountedPrice : c.Price));
            return total;
        }

        public async Task ShoppingSaveForLaterAsync(int itemId)
        {
            var item = _dbContext.ShoppingCart
                .First(c => c.ItemId == itemId);

            item.BuyNow = 0;
            item.Quantity = 1;

            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task MoveItemToCartAsync(int itemId)
        {
            var item = _dbContext
                .ShoppingCart
                .First(c => c.ItemId == itemId);

            item.BuyNow = 1;
            item.AddedOn = DateTime.Now;

            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ShoppingCart>> GetSavedCartItemsAsync(string cartId)
        {
            var products = from pc in _dbContext.ShoppingCart
                join c in _dbContext.Product on pc.ProductId equals c.ProductId
                where pc.CartId == cartId
                      && pc.BuyNow == 0
                select pc;

            return await products.ToListAsync();
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var cart = _dbContext
                .ShoppingCart
                .Where(c => c.ItemId == itemId);
            foreach (var item in cart) 
                _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }
}
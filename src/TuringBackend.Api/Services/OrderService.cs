using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly TuringBackendContext _dbContext;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderService(TuringBackendContext dbContext, IShoppingCartService shoppingCartService)
        {
            _dbContext = dbContext;
            _shoppingCartService = shoppingCartService;
        }
        
        public async Task<List<Order>> GetOrderByIdAsync(int orderId)
        {
            var orders = from o in _dbContext.Orders
                join c in _dbContext.Customer on o.CustomerId equals c.CustomerId
                where o.OrderId == orderId
                select new Order
                {
                    OrderId = o.OrderId,
                    Name = c.Name,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    ShippedOn = o.ShippedOn,
                    CreatedOn = o.CreatedOn
                };
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrderByCustomerIdAsync(int customerId)
        {
            var orders = from o in _dbContext.Orders
                join c in _dbContext.Customer on o.CustomerId equals c.CustomerId
                where o.CustomerId == customerId
                select new Order
                {
                    OrderId = o.OrderId,
                    Name = c.Name,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    ShippedOn = o.ShippedOn,
                    CreatedOn = o.CreatedOn
                };
            return await orders.ToListAsync();
        }

        public async Task<int> SaveOrderAsync(int customerId, string cartId, int shippingId, int taxId)
        {
            var order = new Orders
            {
                TotalAmount = await _shoppingCartService.GetCartTotalAmountAsync(cartId),
                Status = 0,
                ShippingId = shippingId,
                TaxId = taxId,
                CustomerId = customerId,
                CreatedOn = DateTime.Now
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Line items
            var cartItems = await _shoppingCartService.GetShoppingCartByIdAsync(cartId);
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ItemId = cartItem.ItemId,
                    ProductId = cartItem.ProductId,
                    Attributes = cartItem.Attributes,
                    Quantity = cartItem.Quantity,
                    ProductName = cartItem.Name,
                    UnitCost = decimal.Parse(cartItem.Price)
                };
                await _dbContext.OrderDetail.AddAsync(orderDetail);
            }
            await _dbContext.SaveChangesAsync();

            //Clear the cart
            await _shoppingCartService.EmptyCartAsync(cartId);
            return order.OrderId;
        }

        public async Task UpdatePaidOrderAsync(string orderId)
        {
            var id = int.Parse(orderId);
            var order = await _dbContext
                .Orders
                .FirstOrDefaultAsync(c => c.OrderId == id);

            // Update the Order with payment
            if (order != null)
            {
                order.AuthCode = "PAID";
                order.Status = OrderStatus.Paid;
                order.ShippedOn = DateTime.Now;

                _dbContext.Entry(order).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderId)
        {
            return await _dbContext
                .OrderDetail
                .FirstOrDefaultAsync(o => o.ItemId == orderId);
        }
    }
}
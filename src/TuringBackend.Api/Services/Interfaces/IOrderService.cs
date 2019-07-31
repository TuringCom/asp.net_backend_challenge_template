using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrderByCustomerIdAsync(int customerId);
        Task<int> SaveOrderAsync(int customerId, string cartId, int shippingId, int taxId);
        Task UpdatePaidOrderAsync(string orderId);
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderId);
    }
}
using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerName);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(Domain.Enums.OrderStatus status);
        Task DeleteAsync(int id);
    }
}

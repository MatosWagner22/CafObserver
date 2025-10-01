using CafObserver.Application.Observers;
using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using CafObserver.Domain.Interfaces;
using CafObserver.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;

        public OrderService(IOrderRepository orderRepository, INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Agregar observadores al orden
            var notificationObserver = new NotificationOrderObserver(_notificationService);
            order.AddObserver(notificationObserver);

            var createdOrder = await _orderRepository.AddAsync(order);
            return createdOrder;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                var oldStatus = order.Status;
                order.UpdateStatus(status);
                await _orderRepository.UpdateAsync(order);
            }
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
        {
            return await _orderRepository.GetPendingOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerName)
        {
            return await _orderRepository.GetOrdersByCustomerAsync(customerName);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}

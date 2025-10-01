using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using CafObserver.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Infrastructure.Notifications
{
    public class SMSNotificationService : INotificationService
    {
        private readonly ILogger<SMSNotificationService> _logger;

        public SMSNotificationService(ILogger<SMSNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyOrderStatusChange(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            // Simular envío de SMS
            _logger.LogInformation($"SMS enviado: Tu pedido #{order.Id} ahora está {newStatus}. Total: ${order.CalculateTotal()}");

            await Task.Delay(100); // Simular tiempo de envío
        }
    }
}

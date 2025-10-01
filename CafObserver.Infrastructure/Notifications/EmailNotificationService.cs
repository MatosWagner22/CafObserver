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
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyOrderStatusChange(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            // Simular envío de email
            _logger.LogInformation($"Email enviado: El estado de tu pedido #{order.Id} ha cambiado de {oldStatus} a {newStatus}. Cliente: {order.CustomerName}");

            await Task.Delay(100); // Simular tiempo de envío
        }
    }
}

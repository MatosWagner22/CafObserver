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
    public class KitchenDisplayNotificationService : INotificationService
    {
        private readonly ILogger<KitchenDisplayNotificationService> _logger;

        public KitchenDisplayNotificationService(ILogger<KitchenDisplayNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyOrderStatusChange(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            // Simular actualización de pantalla en cocina
            if (newStatus == OrderStatus.InPreparation)
            {
                _logger.LogInformation($"Pantalla cocina actualizada: Preparar pedido #{order.Id} para {order.CustomerName}");
            }

            await Task.Delay(50); // Simular tiempo de actualización
        }
    }
}

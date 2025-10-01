using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using CafObserver.Domain.Interfaces;
using CafObserver.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Application.Observers
{
    public class NotificationOrderObserver : IOrderObserver
    {
        private readonly INotificationService _notificationService;

        public NotificationOrderObserver(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void OnOrderStatusChanged(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            // Ejecutar en segundo plano sin esperar
            Task.Run(async () =>
            {
                await _notificationService.NotifyOrderStatusChange(order, oldStatus, newStatus);
            });
        }
    }
}

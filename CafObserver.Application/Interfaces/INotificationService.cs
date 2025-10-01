using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Infrastructure.Interfaces
{
    public interface INotificationService
    {
        Task NotifyOrderStatusChange(Order order, OrderStatus oldStatus, OrderStatus newStatus);
    }
}

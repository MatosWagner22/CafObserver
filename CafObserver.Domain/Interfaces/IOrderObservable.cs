using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Domain.Interfaces
{
    public interface IOrderObservable
    {
        void AddObserver(IOrderObserver observer);
        void RemoveObserver(IOrderObserver observer);
        void NotifyObservers(OrderStatus oldStatus, OrderStatus newStatus);
    }
}

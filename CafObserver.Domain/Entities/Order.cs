using CafObserver.Domain.Enums;
using CafObserver.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Domain.Entities
{
    public class Order : IOrderObservable
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private List<IOrderObserver> _observers = new List<IOrderObserver>();

        public void UpdateStatus(OrderStatus newStatus)
        {
            var oldStatus = Status;
            Status = newStatus;
            NotifyObservers(oldStatus, newStatus);
        }

        public void AddObserver(IOrderObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IOrderObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(OrderStatus oldStatus, OrderStatus newStatus)
        {
            foreach (var observer in _observers)
            {
                observer.OnOrderStatusChanged(this, oldStatus, newStatus);
            }
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
}

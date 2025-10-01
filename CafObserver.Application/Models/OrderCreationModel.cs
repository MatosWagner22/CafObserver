using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Application.Models
{
    public class OrderCreationModel
    {
        public string CustomerName { get; set; }
        public List<OrderItemCreationModel> Items { get; set; } = new List<OrderItemCreationModel>();
    }

    public class OrderItemCreationModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; }
    }
}

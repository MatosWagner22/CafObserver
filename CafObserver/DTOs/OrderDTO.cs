using CafObserver.Domain.Enums;

namespace CafObserver.API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; }
        public decimal Subtotal { get; set; }
    }
}

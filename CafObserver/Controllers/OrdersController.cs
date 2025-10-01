using CafObserver.Application.Models;
using CafObserver.Domain.Entities;
using CafObserver.Domain.Enums;
using CafObserver.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CafObserver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, IProductService productService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreationModel orderModel)
        {
            try
            {
                // Mapear OrderCreationModel a Order
                var order = new Order
                {
                    CustomerName = orderModel.CustomerName,
                    CreatedAt = DateTime.UtcNow
                };

                // Obtener detalles de productos y crear items
                foreach (var itemModel in orderModel.Items)
                {
                    var product = await _productService.GetProductAsync(itemModel.ProductId);
                    if (product == null)
                    {
                        return BadRequest($"Producto con ID {itemModel.ProductId} no encontrado");
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = itemModel.Quantity,
                        SpecialInstructions = itemModel.SpecialInstructions
                    };

                    order.Items.Add(orderItem);
                }

                var createdOrder = await _orderService.CreateOrderAsync(order);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pedido");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedido {OrderId}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado del pedido {OrderId}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Order>>> GetPendingOrders()
        {
            try
            {
                var orders = await _orderService.GetPendingOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos pendientes");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("customer/{customerName}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(string customerName)
        {
            try
            {
                var orders = await _orderService.GetOrdersByCustomerAsync(customerName);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pedidos del cliente {CustomerName}", customerName);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}

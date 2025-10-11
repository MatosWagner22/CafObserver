# CafeteriaApp - Sistema de Gestión con Patrón Observer

## .NET
### Version
`8.0.0`
### Scripts
**Build Script**
> `dotnet build`

**Database Migration**
> `Add-Migration InitialCreate`  
> `Update-Database`

**Run Application**
> `dotnet run`

## Descripción
### Desarrolle un sistema de gestión para cafetería que implemente el patrón Observer para notificaciones automáticas:

El sistema debe:
- Gestionar productos y pedidos de la cafetería ✔
- Implementar el patrón Observer para notificaciones automáticas ✔
- Notificar cambios de estado a múltiples sistemas simultáneamente ✔
- Ser extensible para nuevos tipos de notificación sin modificar código existente ✔

#### Solución

##### Solution Structure

```
CafeteriaApp/
├── CafeteriaApp.API/ (Capa de Presentación)
│ ├── Controllers/
│ │ ├── OrdersController.cs
│ │ └── ProductsController.cs
│ ├── DTOs/
│ │ └── OrderDTO.cs
│ ├── Middleware/
│ │ └── ExceptionHandlingMiddleware.cs
│ ├── Program.cs
│ └── appsettings.json
├── CafeteriaApp.Application/ (Capa de Lógica de Negocio)
│ ├── Interfaces/
│ │ ├── IOrderService.cs
│ │ ├── IProductService.cs
│ │ └── INotificationService.cs
│ ├── Services/
│ │ ├── OrderService.cs
│ │ └── ProductService.cs
│ ├── Models/
│ │ └── OrderCreationModel.cs
│ ├── Validators/
│ │ └── OrderValidator.cs
│ └── Observers/
│ └── NotificationOrderObserver.cs
├── CafeteriaApp.Infrastructure/ (Capa de Infraestructura)
│ ├── Data/
│ │ └── ApplicationDbContext.cs
│ ├── Repository/
│ │ ├── OrderRepository.cs
│ │ └── ProductRepository.cs
│ └── Notifications/
│ ├── EmailNotificationService.cs
│ ├── SMSNotificationService.cs
│ └── KitchenDisplayNotificationService.cs
└── CafeteriaApp.Domain/ (Capa de Dominio)
├── Entities/
│ ├── Order.cs
│ ├── OrderItem.cs
│ └── Product.cs
├── Interfaces/
│ ├── IOrderObservable.cs
│ ├── IOrderObserver.cs
│ ├── IOrderRepository.cs
│ └── IProductRepository.cs
└── Enums/
├── OrderStatus.cs
└── ProductCategory.cs
```

#### Implementación del Patrón Observer
##### Componentes del Patrón:
- **Sujeto (Observable)**: `Order` - Notifica cambios de estado
- **Observadores**: Servicios de notificación que reaccionan a los cambios
- **Interfaces**: `IOrderObservable` e `IOrderObserver` para el desacoplamiento

##### Flujo del Patrón:
1. ✅ El pedido cambia de estado
2. ✅ Notifica automáticamente a todos los observadores registrados
3. ✅ Cada observador ejecuta su lógica específica
4. ✅ Sistema extensible sin modificar el código core

#### Ejemplos de requests de consumo (API REST)

##### Crear un nuevo pedido (POST /api/orders)
```json
{
  "customerName": "María González",
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "specialInstructions": "Sin azúcar"
    },
    {
      "productId": 3,
      "quantity": 1
    }
  ]
}
```


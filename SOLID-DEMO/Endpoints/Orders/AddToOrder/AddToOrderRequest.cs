using Application.UnitOfWork;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.AddToOrder;

public class AddToOrderRequest : IHttpRequest
{
    public Guid OrderId { get; }
    public CreateOrUpdateOrderDto Cart { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
}
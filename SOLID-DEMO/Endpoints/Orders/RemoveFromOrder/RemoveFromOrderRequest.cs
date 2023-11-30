using Application.UnitOfWork;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.RemoveFromOrder;

public class RemoveFromOrderRequest : IHttpRequest
{
    public IUnitOfWork UnitOfWork { get; set; }

    public Guid OrderId { get; set; }
    public CreateOrUpdateOrderDto Cart { get; set; }

}
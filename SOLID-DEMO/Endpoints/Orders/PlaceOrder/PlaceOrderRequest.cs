using Application.UnitOfWork;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.PlaceOrder;

public class PlaceOrderRequest : IHttpRequest
{
    public IUnitOfWork UnitOfWork { get; set; }

    public CreateOrUpdateOrderDto Cart { get; set; }

}
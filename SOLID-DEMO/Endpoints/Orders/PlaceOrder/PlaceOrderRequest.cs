using Application.UnitOfWork;
using Shared;

namespace Server.Endpoints.Orders.PlaceOrder;

public class PlaceOrderRequest : IHttpRequest
{
    public IUnitOfWork UnitOfWork { get; set; }

    public CustomerCart Cart { get; set; }

}
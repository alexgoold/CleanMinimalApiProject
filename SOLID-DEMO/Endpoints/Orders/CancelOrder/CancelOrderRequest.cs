using Application.UnitOfWork;

namespace Server.Endpoints.Orders.CancelOrder;

public class CancelOrderRequest : IHttpRequest
{
    public IUnitOfWork UnitOfWork { get; set; }

    public Guid OrderId { get; set; }
}
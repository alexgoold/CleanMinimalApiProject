using Application.UnitOfWork;

namespace Server.Endpoints.Orders.RemoveFromOrder;

public class RemoveFromOrderRequest : IHttpRequest
{
	public IUnitOfWork UnitOfWork { get; set; }

	public Guid OrderId { get; set; }


	
}
using Application.UnitOfWork;

namespace Server.Endpoints.Orders.GetAll;

public class GetAllOrdersRequest : IHttpRequest
{
	public IUnitOfWork UnitOfWork { get; set; }
}
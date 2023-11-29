using Application.UnitOfWork;

namespace Server.Endpoints.Customers.GetAll;

public class GetAllCustomersRequest : IHttpRequest
{
	public IUnitOfWork UnitOfWork { get; set; }
}
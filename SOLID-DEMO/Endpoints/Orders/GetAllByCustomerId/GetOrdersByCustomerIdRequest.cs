using Application.UnitOfWork;

namespace Server.Endpoints.Orders.GetAllByCustomerId
{
	public class GetOrdersByCustomerIdRequest : IHttpRequest
	{
		public Guid CustomerId { get; set; }
		public IUnitOfWork UnitOfWork { get; set; }
	}
}

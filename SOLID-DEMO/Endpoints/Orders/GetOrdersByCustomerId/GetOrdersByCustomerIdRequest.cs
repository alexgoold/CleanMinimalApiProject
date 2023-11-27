using Application.UnitOfWork;

namespace Server.Endpoints.Orders.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdRequest : IHttpRequest
    {
        public Guid CustomerId { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
    }
}

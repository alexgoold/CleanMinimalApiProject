using Application.UnitOfWork;

namespace Server.Endpoints.Customers.Delete;

public class DeleteCustomerByIdRequest : IHttpRequest
{
    public Guid Id { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }

}
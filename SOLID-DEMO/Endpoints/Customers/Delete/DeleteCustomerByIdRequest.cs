using Application.UnitOfWork;

namespace Server.Endpoints.Customers.Delete;

public class DeleteCustomerByIdRequest
{
    public Guid Id { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }

}
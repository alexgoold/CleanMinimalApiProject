using Application.UnitOfWork;
using Shared.CustomerDtos;

namespace Server.Endpoints.Customers.Register;

public class RegisterCustomerRequest : IHttpRequest
{
    public IUnitOfWork UnitOfWork { get; set; }

    public CustomerDto Customer { get; set; }
}
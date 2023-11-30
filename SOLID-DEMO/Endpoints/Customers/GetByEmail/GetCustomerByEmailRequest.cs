using Application.UnitOfWork;

namespace Server.Endpoints.Customers.GetByEmail;

public class GetCustomerByEmailRequest : IHttpRequest
{
    public string Email { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }

}
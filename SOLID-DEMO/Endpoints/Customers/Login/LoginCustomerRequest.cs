using Application.UnitOfWork;

namespace Server.Endpoints.Customers.Login
{
    public class LoginCustomerRequest : IHttpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
    }
}

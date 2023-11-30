using Server.Endpoints.Customers.GetAll;
using Server.Endpoints.Customers.GetByEmail;
using Server.Endpoints.Customers.Register;

namespace Server.Extensions.CustomerEndpoints;

public static class WebApplicationCustomerEndpointExtensions
{
    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        app.MediateGet<GetAllCustomersRequest>("customers/getAllCustomers");
        app.MediateGet<GetCustomerByEmailRequest>("customers/getCustomerByEmail");
        app.MediatePost<RegisterCustomerRequest>("customers/registerCustomer");
        return app;
    }
}
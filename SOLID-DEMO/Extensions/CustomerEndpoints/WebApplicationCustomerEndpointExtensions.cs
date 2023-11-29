using Server.Endpoints.Customers.GetAll;

namespace Server.Extensions.CustomerEndpoints;

public static class WebApplicationCustomerEndpointExtensions
{
	public static WebApplication MapCustomerEndpoints(this WebApplication app)
	{
		app.MediateGet<GetAllCustomersRequest>("customers/getAllCustomers");
		return app;
	}
}
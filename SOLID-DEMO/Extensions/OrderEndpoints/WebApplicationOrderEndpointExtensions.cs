using Server.Endpoints.Orders.GetAll;
using Server.Endpoints.Orders.GetAllByCustomerId;

namespace Server.Extensions.OrderEndpoints;

public static class WebApplicationOrderEndpointExtensions
{
	public static WebApplication MapOrderEndpoints(this WebApplication app)
	{
		app.MediateGet<GetAllOrdersRequest>("orders/getAllOrders");
		app.MediateGet<GetOrdersByCustomerIdRequest>("orders/getOrderByCustomerId");
		return app;
	}
}
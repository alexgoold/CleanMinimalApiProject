using Server.Mediator.Products.Get;

namespace Server.Extensions.ProductEndpoints
{
	public static class WebApplicationProductEndpointsExtensions
	{
		public static WebApplication MapProductEndpoints(this WebApplication app)
		{
			app.MediateGet<GetProductRequest>("getProduct");
			return app;
		}
	}
}

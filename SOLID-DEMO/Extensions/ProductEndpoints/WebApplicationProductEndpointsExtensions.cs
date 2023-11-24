using Server.Endpoints.Products.Get;
using Server.Endpoints.Products.GetAll;

namespace Server.Extensions.ProductEndpoints
{
    public static class WebApplicationProductEndpointsExtensions
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            app.MediateGet<GetProductRequest>("getProduct");
            app.MediateGet<GetAllProductsRequest>("getAllProducts");
            return app;
        }
    }
}

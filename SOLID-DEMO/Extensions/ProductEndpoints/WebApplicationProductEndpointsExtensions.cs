using Server.Endpoints.Products.Add;
using Server.Endpoints.Products.Get;
using Server.Endpoints.Products.GetAll;

namespace Server.Extensions.ProductEndpoints
{
    public static class WebApplicationProductEndpointsExtensions
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            app.MediateGet<GetProductRequest>("products/getProduct");
            app.MediateGet<GetAllProductsRequest>("products/getAllProducts");
            app.MediatePost<AddProductRequest>("products/addProduct");
            return app;
        }
    }
}

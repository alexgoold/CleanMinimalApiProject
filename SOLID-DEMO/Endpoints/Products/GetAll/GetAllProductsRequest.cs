using Application.UnitOfWork;

namespace Server.Endpoints.Products.GetAll
{
    public class GetAllProductsRequest : IHttpRequest
    {
        public IUnitOfWork UnitOfWork { get; set; }
    }
}

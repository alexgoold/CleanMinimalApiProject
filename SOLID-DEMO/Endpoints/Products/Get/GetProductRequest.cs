using Application.UnitOfWork;

namespace Server.Endpoints.Products.Get;

public class GetProductRequest : IHttpRequest
{
    public Guid ProductId { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
}
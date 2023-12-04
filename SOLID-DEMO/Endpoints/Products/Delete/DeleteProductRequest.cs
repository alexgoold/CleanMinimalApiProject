using Application.UnitOfWork;

namespace Server.Endpoints.Products.Delete;

public class DeleteProductRequest : IHttpRequest
{
    public Guid Id { get; }
    public IUnitOfWork UnitOfWork { get; set; }

}
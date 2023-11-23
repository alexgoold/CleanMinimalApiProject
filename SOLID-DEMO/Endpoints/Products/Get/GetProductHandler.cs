using MediatR;

namespace Server.Endpoints.Products.Get
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, IResult>
    {
        public async Task<IResult> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await request.UnitOfWork.Products.GetAsync(request.ProductId);

            return Results.Ok(product);
        }
    }
}

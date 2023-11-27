using AutoMapper;
using MediatR;
using Shared.ProductsDtos;

namespace Server.Endpoints.Products.Get
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, IResult>
    {
        private readonly IMapper _mapper;

        public GetProductHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IResult> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await request.UnitOfWork.Products.GetAsync(request.ProductId);

            if (product == null)
            {
                return Results.NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Results.Ok(productDto);
        }
    }
}

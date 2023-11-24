using AutoMapper;
using MediatR;
using Shared.ProductsDtos;

namespace Server.Endpoints.Products.GetAll
{
	public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IResult>
	{
		private readonly IMapper _mapper;

		public GetAllProductsHandler(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IResult> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			var products = await request.UnitOfWork.Products.GetAllAsync();
			var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
			return Results.Ok(productDtos);
		}

	}
}

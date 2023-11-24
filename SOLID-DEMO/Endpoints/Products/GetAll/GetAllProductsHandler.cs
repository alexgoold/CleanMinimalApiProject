using AutoMapper;
using Shared.ProductsDtos;

namespace Server.Endpoints.Products.GetAll
{
	public class GetAllProductsHandler
	{
		private readonly IMapper _mapper;

		public GetAllProductsHandler(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IResult> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			var products = await request.UnitOfWork.Products.GetAllAsync();
			var productDtos = _mapper.Map<List<ProductDto>>(products);
			return Results.Ok(new List<ProductDto>());
		}

	}
}

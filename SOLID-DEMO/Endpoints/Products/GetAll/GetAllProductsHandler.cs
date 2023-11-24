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

		public async Task<List<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			return new List<ProductDto>();
		}

	}
}

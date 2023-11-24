using AutoMapper;

namespace Server.Endpoints.Products.GetAll
{
	public class GetAllProductsHandler
	{
		private readonly IMapper _mapper;

		public GetAllProductsHandler(IMapper mapper)
		{
			_mapper = mapper;
		}

	}
}

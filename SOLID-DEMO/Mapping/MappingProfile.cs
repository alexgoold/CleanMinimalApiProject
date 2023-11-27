using AutoMapper;
using Domain;
using Shared.ProductsDtos;

namespace Server.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductDto>();
			CreateMap<ProductDto, Product>();
		}
	}
}

using Application.UnitOfWork;
using Shared.ProductsDtos;

namespace Server.Endpoints.Products.Add
{
	public class AddProductRequest
	{
		public ProductDto ProductDto { get; set; }
		public IUnitOfWork UnitOfWork { get; set; }
	}
}

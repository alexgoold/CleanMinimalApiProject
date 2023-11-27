using Application.UnitOfWork;
using Shared.ProductsDtos;

namespace Server.Endpoints.Products.Add
{
	public class AddProductRequest : IHttpRequest
	{
		public ProductDto ProductDto { get; set; }
		public IUnitOfWork UnitOfWork { get; set; }
	}
}

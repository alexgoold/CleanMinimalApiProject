using AutoMapper;
using Domain;
using MediatR;

namespace Server.Endpoints.Products.Add;

public class AddProductHandler: IRequestHandler<AddProductRequest, IResult>
{
	private readonly IMapper _mapper;

	public AddProductHandler(IMapper mapper)
	{
		_mapper = mapper;
	}
	public async Task<IResult> Handle(AddProductRequest request, CancellationToken cancellationToken)
	{
		var product = _mapper.Map<Product>(request.ProductDto);
		if (string.IsNullOrEmpty(product.Name))
		{
			return Results.BadRequest();
		}

		await request.UnitOfWork.Products.AddAsync(product);
		await request.UnitOfWork.SaveChangesAsync();

		return Results.Ok();
	}
	
}
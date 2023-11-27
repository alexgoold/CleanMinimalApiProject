using AutoMapper;

namespace Server.Endpoints.Products.Add;

public class AddProductHandler
{
	private readonly IMapper _mapper;

	public AddProductHandler(IMapper mapper)
	{
		_mapper = mapper;
	}
	public async Task<IResult> Handle(AddProductRequest request, CancellationToken cancellationToken)
	{
		return Results.Ok();
	}
	
}
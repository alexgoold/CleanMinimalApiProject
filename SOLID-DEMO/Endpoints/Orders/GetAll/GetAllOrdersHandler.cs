using AutoMapper;
using MediatR;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.GetAll;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersRequest, IResult>
{
	private readonly IMapper Mapper;

	public GetAllOrdersHandler(IMapper mapper)
	{
		Mapper = mapper;
	}

	public async Task<IResult> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
	{
		var orders = await request.UnitOfWork.Orders.GetAllAsync();
		var orderDtos = Mapper.Map<IEnumerable<OrderDto>>(orders);

		return Results.Ok(orderDtos);
	}
}
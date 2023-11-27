using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Shared.OrderDtos;

namespace Server.Endpoints.Orders.GetAllByCustomerId;

public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdRequest, IResult>
{
	private readonly IMapper _mapper;
	public GetOrdersByCustomerIdHandler(IMapper mapper)
	{
		_mapper = mapper;
	}
	public async Task<IResult> Handle(GetOrdersByCustomerIdRequest request, CancellationToken cancellationToken)
	{
		var orders = await request.UnitOfWork.Orders.GetOrdersForCustomerAsync(request.CustomerId);
		if (orders.IsNullOrEmpty())
		{
			return Results.NotFound();
		}
		var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
		return Results.Ok(orderDtos);
		
	}
}
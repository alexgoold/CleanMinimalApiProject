using MediatR;

namespace Server.Endpoints.Orders.RemoveFromOrder;

public class RemoveFromOrderHandler : IRequestHandler<RemoveFromOrderRequest, IResult>
{
	public async Task<IResult> Handle(RemoveFromOrderRequest dummyRequest, CancellationToken none)
	{

		return Results.Ok();
	}
}
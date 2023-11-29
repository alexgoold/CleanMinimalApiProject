using Application.UnitOfWork;
using Domain;
using MediatR;

namespace Server.Endpoints.Orders.AddToOrder;

public class AddToOrderHandler : IRequestHandler<AddToOrderRequest, IResult>
{
	public async Task<IResult> Handle(AddToOrderRequest request, CancellationToken cancellationToken)
	{
		return Results.Ok();
	}
}
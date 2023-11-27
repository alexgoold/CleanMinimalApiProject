using MediatR;

namespace Server.Endpoints.Orders.CancelOrder;

public class CancelOrderHandler : IRequestHandler<CancelOrderRequest, IResult>
{
    public async Task<IResult> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
    {
        var orderToDelete = await request.UnitOfWork.Orders.GetAsync(request.OrderId);

        if (orderToDelete == null)
            return Results.NotFound();

        await request.UnitOfWork.Orders.DeleteAsync(orderToDelete);
        await request.UnitOfWork.SaveChangesAsync();

        return Results.Ok();
    }
}
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Server.Endpoints.Orders.RemoveFromOrder;

public class RemoveFromOrderHandler : IRequestHandler<RemoveFromOrderRequest, IResult>
{
	public async Task<IResult> Handle(RemoveFromOrderRequest request, CancellationToken none)
	{
		var order = await request.UnitOfWork.Orders.GetAsync(request.OrderId);
		if (order == null)
			return Results.NotFound();

		if (order.Products.IsNullOrEmpty())
			return Results.BadRequest();
		
		foreach (var id in request.Cart.ProductIds)
		{
			var prod = await request.UnitOfWork.Products.GetAsync(id);
			if (prod == null)
				return Results.NotFound();
		}

		var distinctProductsInCartAndTheirQuantities = request.Cart.ProductIds
			.Distinct()
			.Select(id => new
			{
				ProductId = id,
				Quantity = request.Cart.ProductIds.Count(p => p == id)
			});

		foreach (var item in distinctProductsInCartAndTheirQuantities)
		{
			order.Products
				.Where(p => p.ProductId == item.ProductId)
				.ToList()
				.ForEach(p => p.Quantity -= item.Quantity);

			if (order.Products
				    .Where(p => p.ProductId == item.ProductId)
				    .Sum(p => p.Quantity) <= 0)

				order.Products
					.RemoveAll(p => p.ProductId == item.ProductId);
		}

		await request.UnitOfWork.Orders.UpdateAsync(order);
		await request.UnitOfWork.SaveChangesAsync();



		return Results.Ok();


		return Results.Ok();
	}
}
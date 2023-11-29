using Domain;
using MediatR;

namespace Server.Endpoints.Orders.AddToOrder;

public class AddToOrderHandler : IRequestHandler<AddToOrderRequest, IResult>
{
	public async Task<IResult> Handle(AddToOrderRequest request, CancellationToken cancellationToken)
	{
		var order = await request.UnitOfWork.Orders.GetAsync(request.OrderId);
		if (order == null)
			return Results.NotFound();

		var customer = await request.UnitOfWork.Customers.GetAsync(request.Cart.CustomerId);
		if (customer == null)
			return Results.NotFound();

		foreach (var id in request.Cart.ProductIds)
		{
			var prod = await request.UnitOfWork.Products.GetAsync(id);
			if (prod == null)
				return Results.NotFound();
		}

		order.Customer = customer;
		order.ShippingDate = DateTime.Now.AddDays(5);
		order.Products = new List<OrderItem>();

		var distinctProductsInCartAndTheirQuantities = request.Cart.ProductIds
			.Distinct()
			.Select(id => new
			{
				ProductId = id,
				Quantity = request.Cart.ProductIds.Count(p => p == id)
			});

		foreach (var item in distinctProductsInCartAndTheirQuantities)
		{
			order.Products.Add(new OrderItem()
			{
				ProductId = item.ProductId,
				Quantity = item.Quantity
			});
		}

		await request.UnitOfWork.Orders.UpdateAsync(order);
		await request.UnitOfWork.SaveChangesAsync();



		return Results.Ok();
		
	}
}
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Server.Endpoints.Orders.PlaceOrder
{
	public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, IResult>
	{
		public async Task<IResult> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
		{
			var customer = await request.UnitOfWork.Customers.GetAsync(request.Cart.CustomerId);
			if (customer == null)
			{
				return Results.NotFound();
			}

			foreach (var id in request.Cart.ProductIds)
			{
				var prod = await request.UnitOfWork.Products.GetAsync(id);
				if (prod == null)
				{
					return Results.NotFound();
				}
				
			}

			var distinctProductsInCartAndTheirQuantities = request.Cart.ProductIds
				.Distinct()
				.Select(id => new
				{
					ProductId = id, 
					Quantity = request.Cart.ProductIds.Count(p => p == id)
				});

			var order = new Order();
			order.Customer = customer;
			order.ShippingDate = DateTime.Now.AddDays(5);
			order.Products = new List<OrderItem>();
			
			foreach (var item in distinctProductsInCartAndTheirQuantities)
			{
				order.Products.Add(new OrderItem()
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				});
			}
			
			await request.UnitOfWork.Orders.AddAsync(order);
			await request.UnitOfWork.SaveChangesAsync();
			return Results.Ok();
		}
	}
}

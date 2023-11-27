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
			if (customer is null) return Results.NotFound();

			var products = new List<Product>();
			foreach (var productId in request.Cart.ProductIds)
			{
				var prod = await request.UnitOfWork.Products.GetAsync(productId);
				if (prod is null)
				{
					return Results.NotFound();
				}
				products.Add(prod);
			}

			var order = new Order() { 
				Customer = customer,
				Products = products,
				ShippingDate = DateTime.Now.AddDays(5)

			};

			await request.UnitOfWork.Orders.AddAsync(order);
			await request.UnitOfWork.SaveChangesAsync();

			return Results.Ok();

		}
	}
}

using Domain;

namespace Tests.Helpers;

public static class OrderGenerator
{
	public static Order GenerateOrder()
	{
		return new Order()
		{
			Id = Guid.NewGuid(),
			Products = ProductGenerator.GenerateListOf3Products(),
			Customer = CustomerGenerator.GenerateCustomer(),
			ShippingDate = DateTime.Now
		};
	}

	public static List<Order> Generate3Orders()
	{
		return new List<Order>
		{
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = CustomerGenerator.GenerateCustomer(),
				ShippingDate = DateTime.Now
			},
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = CustomerGenerator.GenerateCustomer(),
				ShippingDate = DateTime.Now
			},
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = CustomerGenerator.GenerateCustomer(),
				ShippingDate = DateTime.Now
			}
		};
	}

	public static List<Order> Generate3OrdersForOneCustomer()
	{
		var customer = CustomerGenerator.GenerateCustomer();
		return new List<Order>
		{
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = customer,
				ShippingDate = DateTime.Now
			},
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = customer,
				ShippingDate = DateTime.Now
			},
			new()
			{
				Id = Guid.NewGuid(),
				Products = ProductGenerator.GenerateListOf3Products(),
				Customer = customer,
				ShippingDate = DateTime.Now
			}
		};
	}
}
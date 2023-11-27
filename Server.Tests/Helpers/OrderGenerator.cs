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
}
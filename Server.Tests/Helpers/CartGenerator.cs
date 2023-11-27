using Domain;
using Shared;

namespace Tests.Helpers;

public static class CartGenerator
{
	public static CustomerCart GenerateCartWith3Items()
	{
		return new CustomerCart()
		{
			CustomerId = Guid.NewGuid(),
			ProductIds = new List<Guid>()
			{
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
			}
			
		};
	}
}
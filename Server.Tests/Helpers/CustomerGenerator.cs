using Domain;

namespace Tests.Helpers;

public static class CustomerGenerator
{
	public static Customer GenerateCustomer()
	{
		return new Customer()
		{
			Id = Guid.NewGuid(),
			Name = "Test Customer",
			Password = "Test Password"
		};
	}
}
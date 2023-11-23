using Domain;
using Infrastructure.DataContext;

namespace Tests.InMemoryDb;

public class OrderDatabase
{
	public static void SeedDatabaseWith3Orders(ShopContext context)
	{
		if (!context.Orders.Any())
		{
			context.Orders.AddRange(
				new Order()
				{
					Id = Guid.NewGuid(),
					Products = new List<Product>(),
					Customer = new Customer("John", "123321"),
					ShippingDate = DateTime.Now.AddDays(1),

				},
				new Order()
				{
					Id = Guid.NewGuid(),
					Products = new List<Product>(),
					Customer = new Customer("Johnny", "123321"),
					ShippingDate = DateTime.Now.AddDays(1),
				},
				new Order()
				{
					Id = Guid.NewGuid(),
					Products = new List<Product>(),
					Customer = new Customer("Tim", "123321"),
					ShippingDate = DateTime.Now.AddDays(1),
				}


			);

			context.SaveChanges();
		}
		else
		{
			Console.WriteLine("Data already exists");
		}
	}

	public static void SeedDatabaseWithSingleOrder(ShopContext context, Order order)
	{
		context.Orders.Add(order);
		context.SaveChanges();
	}
}
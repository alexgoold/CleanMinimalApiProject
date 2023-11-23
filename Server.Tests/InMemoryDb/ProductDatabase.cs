using Domain;
using Infrastructure.DataContext;

namespace Tests.InMemoryDb;

public class ProductDatabase
{
	public static void SeedDatabase(ShopContext context)
	{
		if (!context.Products.Any())
		{
			context.Products.AddRange(
				new Product()
				{
					Id = Guid.NewGuid(),
					Name = "Computer",
					Description = "This is a computer"

				},
				new Product()
				{
					Id = Guid.NewGuid(),
					Name = "Laptop",
					Description = "This is a laptop"
				},
				new Product()
				{
					Id = Guid.NewGuid(),
					Name = "Phone",
					Description = "This is a phone"
				}

			);

			context.SaveChanges();
		}
		else
		{
			Console.WriteLine("Data already exists");
		}
	}
}
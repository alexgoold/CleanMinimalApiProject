using Domain;
using Infrastructure.DataContext;

namespace Tests.InMemoryDb;

public class CustomerDatabase
{
	public static void SeedDatabaseWithSingleCustomer(ShopContext context, Customer customer)
	{
		context.Customers.Add(customer);
		context.SaveChanges();
	}

	public static void EmptyDatabase(ShopContext context)
	{
		context.Customers.RemoveRange(context.Customers);
		context.SaveChanges();
	}
}
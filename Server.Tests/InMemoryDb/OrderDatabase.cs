using Domain;
using Infrastructure.DataContext;

namespace Tests.InMemoryDb;

public class OrderDatabase
{
    public static void SeedDatabaseWithSingleOrder(ShopContext context, Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();
    }

    public static void SeedDatabaseWithMultipleOrders(ShopContext context, List<Order> orders)
    {
		context.Orders.AddRange(orders);
		context.SaveChanges();
	}

    public static void EmptyDatabase(ShopContext context)
    {
        context.Orders.RemoveRange(context.Orders);
		context.SaveChanges();
    }
}
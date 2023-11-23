using Domain;
using Infrastructure.DataContext;

namespace Tests.InMemoryDb;

public class ProductDatabase
{
    public static void SeedDatabaseWith3Products(ShopContext context)
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

    public static void SeedDatabaseWithSingleProduct(ShopContext context, Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public static void EmptyDatabase(ShopContext context)
    {
        context.Products.RemoveRange(context.Products);
        context.SaveChanges();
    }
}
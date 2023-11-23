using Domain;
using FluentAssertions;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Tests.Helpers;
using Tests.InMemoryDb;
using Xunit;

namespace Tests.Repositories;

public class Order_Repository_Tests
{

    private OrderRepository _sut = null!;
    private ShopContext _context = null!;
    private DbContextOptions<ShopContext> _dbContextOptions = null!;

    public Order_Repository_Tests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "OrderDbTest")
            .Options;

        _context = new ShopContext(_dbContextOptions);

        _sut = new OrderRepository(_context);
    }


}
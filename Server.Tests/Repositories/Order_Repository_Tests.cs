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

    #region GetAllOrders

    [Fact]
    public async Task GetAllOrders_WhenCalled_Returns_ListOfOrders()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeOfType<List<Order>>();
    }

    [Fact]
    public async Task GetAllOrders_WhenCalled_With_No_ProductsInDb_Returns_EmptyList()
    {
        // Arrange
        OrderDatabase.EmptyDatabase(_context);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllOrders_WhenCalled_With_One_OrderInDb_Returns_ListWithOneOrder()
    {
        // Arrange
        OrderDatabase.EmptyDatabase(_context);
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllOrders_WhenCalled_ReturnsOrders_With_Customers()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().AllBeOfType<Order>();
        result.First().Customer.Should().BeOfType<Customer>();
    }

    [Fact]
    public async Task GetAllOrders_WhenCalled_ReturnsOrders_With_Products()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().AllBeOfType<Order>();
        result.First().Products.Should().AllBeOfType<OrderItem>();
    }

    #endregion

    #region GetOrdersForCustomer

    [Fact]
    public async Task GetOrdersForCustomer_WhenCalled_WithCustomerId_ShouldReturn_ListOfOrders()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetOrdersForCustomerAsync(order.Customer.Id);

        // Assert
        result.Should().BeOfType<List<Order>>();
    }

    [Fact]
    public async Task GetOrdersForCustomer_WhenCalled_WithCustomerId_ShouldReturn_ListOfOrders_With_Customer()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetOrdersForCustomerAsync(order.Customer.Id);

        // Assert
        result.Should().AllBeOfType<Order>();
        result.First().Customer.Should().BeOfType<Customer>();
    }

    [Fact]
    public async Task GetOrdersForCustomer_WhenCalled_WithCustomerId_ShouldReturn_ListOfOrders_With_Products()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetOrdersForCustomerAsync(order.Customer.Id);

        // Assert
        result.Should().AllBeOfType<Order>();
        result.First().Products.Should().AllBeOfType<OrderItem>();
    }

    [Fact]
    public async Task
        GetOrdersForCustomer_WhenCalled_With3OrdersForCustomer_InDb_AndOneOrderForDifferentCustomerInDb_ShouldReturn_ListOfOrders_With_Count_3()
    {
        // Arrange
        OrderDatabase.EmptyDatabase(_context);
        var orders = OrderGenerator.Generate3OrdersForOneCustomer();
        OrderDatabase.SeedDatabaseWithMultipleOrders(_context, orders);

        var orderForDifferentCustomer = OrderGenerator.GenerateOrder();
        orderForDifferentCustomer.Customer = new Customer() { Id = Guid.NewGuid(), Email = "new@example.com", Password = "123" };
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, orderForDifferentCustomer);

        // Act
        var allOrders = await _sut.GetAllAsync();
        var result = await _sut.GetOrdersForCustomerAsync(orders.First().Customer.Id);

        // Assert
        allOrders.Should().HaveCount(4);
        result.Should().HaveCount(3);
    }

    #endregion

    #region AddOrder

    [Fact]
    public async Task AddOrder_WhenCalled_WithValidOrder_ShouldAddOrderToDb()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();


        // Act
        await _sut.AddAsync(order);
        await _context.SaveChangesAsync();

        // Assert
        _context.Orders.Should().Contain(order);
    }

    #endregion

    #region DeleteOrder

    [Fact]
    public async Task DeleteOrder_WhenCalled_WithValidOrder_ShouldDeleteOrderFromDb()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        await _sut.DeleteAsync(order);
        await _context.SaveChangesAsync();

        // Assert
        _context.Orders.Should().NotContain(order);
    }

    #endregion

    #region UpdateOrder

    [Fact]
    public async Task UpdateOrder_WhenCalled_WithValidUpdatedOrder_ShouldUpdateOrderInDb()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act

        order.Customer.Email = "newEmail@example.com";
        order.Products.First().Quantity = 5;

        await _sut.UpdateAsync(order);
        await _context.SaveChangesAsync();

        // Assert
        _context.Orders.Should().Contain(order);
    }

    #endregion

    #region GetOrder

    [Fact]
    public async Task GetOrder_WhenCalled_WithValidId_ShouldReturn_Order()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAsync(order.Id);

        // Assert
        result.Should().BeOfType<Order>();
    }

    [Fact]
    public async Task GetOrder_WhenCalled_WithIdOfProduct_NotInDatabase_Returns_Null()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetOrder_WhenCalled_WithInvalidGuid_Returns_Null()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAsync(Guid.Empty);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_WhenCalled_WithSpecifiedId_Returns_SingleOrder_WithMatchingId()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);
        var guid = order.Id;

        // Act
        var result = await _sut.GetAsync(guid);

        // Assert
        result.Id.Should().Be(guid);

    }

    [Fact]
    public async Task GetOrder_WhenCalled_WithValidId_ShouldReturn_Order_With_Customer()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAsync(order.Id);

        // Assert
        result.Should().BeOfType<Order>();
        result.Customer.Should().BeOfType<Customer>();
    }

    [Fact]
    public async Task GetOrder_WhenCalled_WithValidId_ShouldReturn_Order_With_Products()
    {
        // Arrange
        var order = OrderGenerator.GenerateOrder();
        OrderDatabase.SeedDatabaseWithSingleOrder(_context, order);

        // Act
        var result = await _sut.GetAsync(order.Id);

        // Assert
        result.Should().BeOfType<Order>();
        result.Products.Should().AllBeOfType<OrderItem>();
    }

    #endregion
}
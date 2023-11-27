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
		result.First().Products.Should().AllBeOfType<Product>();
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

	#endregion





}
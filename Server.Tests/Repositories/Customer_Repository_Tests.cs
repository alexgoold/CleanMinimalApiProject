using Domain;
using FluentAssertions;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Tests.Helpers;
using Tests.InMemoryDb;
using Xunit;

namespace Tests.Repositories;

public class Customer_Repository_Tests
{
    private CustomerRepository _sut = null!;
    private ShopContext _context = null!;
    private DbContextOptions<ShopContext> _dbContextOptions = null!;

    public Customer_Repository_Tests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
            .UseInMemoryDatabase(databaseName: "CustomerDbTest")
            .Options;

        _context = new ShopContext(_dbContextOptions);

        _sut = new CustomerRepository(_context);
    }

    #region GetAsync Tests

    [Fact]
    public async Task GetAsync_WhenCalled_Returns_SingleCustomer()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);

        // Act
        var result = await _sut.GetAsync(customer.Id);

        // Assert
        result.Should().BeOfType<Customer>();
    }

    [Fact]
    public async Task GetAsync_WhenCalled_WithSpecifiedId_Returns_SingleCustomer_WithMatchingId()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);
        var guid = customer.Id;

        // Act
        var result = await _sut.GetAsync(guid);

        // Assert
        result.Id.Should().Be(guid);

    }

    [Fact]
    public async Task GetAsync_WhenCalled_With_Id_Not_In_Database_Returns_Null()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = await _sut.GetAsync(guid);

        // Assert
        result.Should().BeNull();

    }

    [Fact]
    public async Task GetAsync_WhenCalled_With_InvalidGuid_Returns_Null()
    {
        // Arrange
        var guid = Guid.Empty;

        // Act
        var result = await _sut.GetAsync(guid);

        // Assert
        result.Should().BeNull();

    }

    #endregion
}
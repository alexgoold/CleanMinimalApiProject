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

    #region GetAll Tests

    [Fact]
    public async Task GetAllAsync_WhenCalled_WithCustomersInDB_Returns_ListOfCustomers()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeOfType<List<Customer>>();
    }

    [Fact]
    public async Task GetAllAsync_WhenCalled_WithEmptyDatabase_Returns_EmptyList()
    {
        // Arrange
        CustomerDatabase.EmptyDatabase(_context);

        //Acc
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    #endregion

    #region AddCustomer

    [Fact]
    public async Task AddCustomer_WhenCalled_WithValidCustomer_ShouldAddCustomerToDb()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();


        // Act
        await _sut.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Assert
        _context.Customers.Should().Contain(customer);
    }

    #endregion

    #region DeleteCustomer

    [Fact]
    public async Task DeleteCustomer_WhenCalled_WithValidCustomer_ShouldDeleteCustomerFromDb()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);

        // Act
        await _sut.DeleteAsync(customer);
        await _context.SaveChangesAsync();

        // Assert
        _context.Customers.Should().NotContain(customer);
    }

    #endregion

    #region UpdateCustomer

    [Fact]
    public async Task UpdateCustomer_WhenCalled_WithValidUpdatedCustomer_ShouldUpdateCustomerInDb()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);

        // Act
        customer.Email = "Updated@Update.com";

        await _sut.UpdateAsync(customer);
        await _context.SaveChangesAsync();

        // Assert
        _context.Customers.Should().Contain(customer);
    }

    #endregion

    #region GetByEmail

    [Fact]
    public async Task GetByEmail_WhenCalled_Returns_SingleCustomer()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);

        // Act
        var result = await _sut.GetByEmailAsync(customer.Email);

        // Assert
        result.Should().BeOfType<Customer>();
    }

    [Fact]
    public async Task GetByEmail_WhenCalled_WithSpecifiedEmail_Returns_SingleCustomer_WithMatchingEmail()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        CustomerDatabase.SeedDatabaseWithSingleCustomer(_context, customer);
        var email = customer.Email;

        // Act
        var result = await _sut.GetByEmailAsync(email);

        // Assert
        result.Email.Should().Be(email);

    }

    [Fact]
    public async Task GetByEmail_WhenCalled_With_Email_Not_In_Database_Returns_Null()
    {
        // Arrange
        var email = "ThisisntanEmail@email.com";

        // Act
        var result = await _sut.GetByEmailAsync(email);

        // Assert
        result.Should().BeNull();

    }

    [Fact]
    public async Task GetByEmail_WhenCalled_With_InvalidEmail_Returns_Null()
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
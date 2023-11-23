using Domain;
using FluentAssertions;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Tests.Helpers;
using Tests.InMemoryDb;
using Xunit;

namespace Tests
{
	public class Product_Repository_Tests
	{
		private ProductRepository _sut = null!;
		private ShopContext _context = null!;
		private DbContextOptions<ShopContext> _dbContextOptions = null!;

		public Product_Repository_Tests()
		{
			_dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
				.UseInMemoryDatabase(databaseName: "ProductDbTest")
				.Options;

			_context = new ShopContext(_dbContextOptions);

			_sut = new ProductRepository(_context);
		}

		#region GetAsync Tests

		[Fact]
		public async Task GetAsync_WhenCalled_Returns_SingleProduct()
		{
			// Arrange
			var product = ProductGenerator.GenerateProduct();
			ProductDatabase.SeedDatabeWithSingleProduct(_context, product);

			// Act
			var result = await _sut.GetAsync(product.Id);

			// Assert
			result.Should().BeOfType<Product>();
		}

		[Fact]
		public async Task GetAsync_WhenCalled_WithSpecifiedId_Returns_SingleProduct_WithMatchingId()
		{
			// Arrange
			var product = ProductGenerator.GenerateProduct();
			ProductDatabase.SeedDatabeWithSingleProduct(_context, product);
			var guid = product.Id;

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

		#region GetAllAsync Tests

		[Fact]
		public async Task GetAllAsync_WhenCalled_Returns_ListOfProducts()
		{
			// Arrange


			// Act
			var result = await _sut.GetAllAsync();

			// Assert
			result.Should().BeOfType<List<Product>>();
		}

		[Fact]
		public async Task GetAllAsync_WhenCalled_Returns_ListOfProducts_With_Count_Matching_Number_Of_Products_In_Database()
		{
			// Arrange
			ProductDatabase.SeedDatabaseWith3Products(_context);

			// Act
			var result = await _sut.GetAllAsync();

			// Assert
			result.Count().Should().Be(3);
		}

		[Fact]
		public async Task GetAllAsync_WhenCalled_WithNoProductsInDatabase_Returns_EmptyList()
		{
			// Arrange


			// Act
			var result = await _sut.GetAllAsync();

			// Assert
			result.Should().BeEmpty();
		}

		#endregion

		#region AddAsync Tests

		[Fact]
		public async Task AddAsync_WhenCalled_SavesProduct_ToDatabase()
		{
			// Arrange
			var product = ProductGenerator.GenerateProduct();
			ProductDatabase.SeedDatabeWithSingleProduct(_context, product);

			// Act
			await _sut.AddAsync(product);

			// Assert
			_context.Products.Should().ContainEquivalentOf(product);
		}

		[Fact]
		public async Task AddAsync_WhenCalled_WithProduct_With_Id_Already_In_Database_ThrowsDbUpdateException()
		{
			// Arrange
			var product = ProductGenerator.GenerateProduct();
			ProductDatabase.SeedDatabeWithSingleProduct(_context, product);

			// Act
			var act = async () => await _sut.AddAsync(product);

			// Assert
			await act.Should().ThrowAsync<DbUpdateException>();
		}

		#endregion



	}
}
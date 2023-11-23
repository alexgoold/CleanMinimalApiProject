using Domain;
using FluentAssertions;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Tests.Helpers;
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

        [Fact]
        public async Task GetAsync_WhenCalled_Returns_SingleProduct()
        {
            // Arrange


            // Act
            var result = await _sut.GetAsync(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<Product>();
        }

        [Fact]
        public async Task GetAsync_WhenCalled_WithSpecifiedId_Returns_SingleProduct_WithMatchingId()
        {
            // Arrange
            var product = ProductGenerator.GenerateProduct();
            var guid = product.Id;

            // Act
            var result = await _sut.GetAsync(guid);

            // Assert
            result.Id.Should().Be(guid);

        }
    }
}
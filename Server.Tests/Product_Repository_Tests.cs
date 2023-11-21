using System;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Server.DataAccess;

namespace Server.Tests
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
		public void GetProductById_WhenCalled_ReturnsProductModel()
		{

		}
	}
}
using Server.DataAccess;
using Server.Interfaces;
using System;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly ShopContext _context;

	public ProductRepository(ShopContext context) => _context = context;

	// TODO: Implement CRUD
}
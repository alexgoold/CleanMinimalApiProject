using Application.Interfaces;
using Domain;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetAsync(Guid id)
    {
       return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
	    return new List<Product>();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}
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
        return await _context.Products.ToListAsync();
    }


    public async Task AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
    }

    public async Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
    }
}
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
	    return new List<Product>()
	    {
		    new Product()
		    {
			    Name = "Computer",
			    Description = "This is a computer"
		    },
		    new Product()
		    {
			    Name = "Laptop",
			    Description = "This is a laptop"
		    },
		    new Product()
		    {
			    Name =
				    "Phone",
			    Description = "This is a phone"
		    }
	    };
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
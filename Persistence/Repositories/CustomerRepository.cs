using Application.Interfaces;
using Domain;
using Infrastructure.DataContext;

namespace Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ShopContext _context;

    public CustomerRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Customer entity)
    {
        throw new NotImplementedException();
    }
}
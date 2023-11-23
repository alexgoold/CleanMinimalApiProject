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

    public async Task<Customer> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> AddAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> DeleteAsync(Customer entity)
    {
        throw new NotImplementedException();
    }
}
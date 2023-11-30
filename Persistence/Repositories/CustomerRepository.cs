using Application.Interfaces;
using Domain;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Customers.ToListAsync();
    }

    public async Task AddAsync(Customer entity)
    {
        await _context.Customers.AddAsync(entity);
    }

    public async Task UpdateAsync(Customer entity)
    {
        _context.Customers.Update(entity);
    }

    public async Task DeleteAsync(Customer entity)
    {
        _context.Customers.Remove(entity);
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email));
    }

}
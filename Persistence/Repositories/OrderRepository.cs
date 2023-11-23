using Application.Interfaces;
using Domain;
using Infrastructure.DataContext;

namespace Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _context;

    public OrderRepository(ShopContext context)
    {
        _context = context;
    }
    public async Task<Order?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
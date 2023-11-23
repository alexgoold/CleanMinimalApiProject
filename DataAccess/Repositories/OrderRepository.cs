using Application.Common;
using Infrastructure.DataContext;
using Shared;

namespace Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _context;

    public OrderRepository(ShopContext context)
    {
        _context = context;
    }
    public async Task<Order> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Order> AddAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> DeleteAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
using Application.Interfaces;
using Domain;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Orders
	        .Include(o => o.Customer)
	        .Include(o => o.Products)
	        .ToListAsync();
    }

    public async Task AddAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
    }

    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
    }

    public async Task DeleteAsync(Order entity)
    {
	    _context.Orders.Remove(entity);
    }

    public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(Guid customerId)
    {
	    return await _context.Orders
		    .Include(o => o.Customer)
		    .Include(o => o.Products)
		    .Where(o => o.Customer.Id == customerId)
		    .ToListAsync();
    }
}
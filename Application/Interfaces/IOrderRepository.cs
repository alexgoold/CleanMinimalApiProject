using Domain;

namespace Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
	public Task<IEnumerable<Order>> GetOrdersForCustomerAsync(Guid customerId);
	
}
using Application.Common;
using Infrastructure.DataContext;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopContext _context;
    public UnitOfWork(ShopContext context, ICustomerRepository customerRepository, IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _context = context;
        Customers = customerRepository;
        Orders = orderRepository;
        Products = productRepository;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await
            _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
}
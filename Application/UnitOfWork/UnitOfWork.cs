using Application.Interfaces;
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

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
}
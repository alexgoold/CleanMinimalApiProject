using Application.Interfaces;

namespace Application.UnitOfWork;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();

    public void Dispose();

    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }
    public IProductRepository Products { get; }
}
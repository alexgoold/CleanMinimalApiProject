
using Domain;

namespace Application.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<Customer?> GetByEmailAsync(string email);

}
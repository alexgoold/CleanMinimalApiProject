using Application.Interfaces;
using Application.UnitOfWork;
using Persistence.Repositories;

namespace Server.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDependencyInjection(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

}
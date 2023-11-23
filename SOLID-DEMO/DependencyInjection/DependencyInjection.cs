using Application;
using Application.Interfaces;
using Application.UnitOfWork;
using Persistence.Repositories;
using System.Reflection;

namespace Server.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDependencyInjection(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

	}



}
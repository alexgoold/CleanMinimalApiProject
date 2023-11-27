using Domain;
using Shared.CustomerDtos;

namespace Tests.Helpers;

public static class CustomerGenerator
{
    public static Customer GenerateCustomer()
    {
        return new Customer()
        {
            Id = Guid.NewGuid(),
            Email = "Test@Customer.com",
            Password = "Test Password"
        };
    }

    public static CustomerDto GenerateCustomerDto()
    {
        return new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Test@Customer.com",
            Password = "Test Password"
        };
    }

}
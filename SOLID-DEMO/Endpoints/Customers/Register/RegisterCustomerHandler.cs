using AutoMapper;
using Domain;
using Infrastructure.Security.HashingStrategy;
using MediatR;

namespace Server.Endpoints.Customers.Register;

public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerRequest, IResult>
{
    private readonly IMapper _mapper;
    private readonly IHashingStrategy _passwordHasher;

    public RegisterCustomerHandler(IMapper mapper, IHashingStrategy hasher)
    {
        _mapper = mapper;
        _passwordHasher = hasher;
    }

    public async Task<IResult> Handle(RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request.Customer);
        customer.Password = _passwordHasher.HashPassword(request.Customer.Password);

        await request.UnitOfWork.Customers.AddAsync(customer);
        await request.UnitOfWork.SaveChangesAsync();

        return Results.Ok();
    }
}

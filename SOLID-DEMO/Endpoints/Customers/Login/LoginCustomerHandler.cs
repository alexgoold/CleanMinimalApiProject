using Infrastructure.Security.HashingStrategy;
using MediatR;

namespace Server.Endpoints.Customers.Login;

public class LoginCustomerHandler : IRequestHandler<LoginCustomerRequest, IResult>
{
    private readonly IHashingStrategy _passwordHasher;

    public LoginCustomerHandler(IHashingStrategy passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<IResult> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await request.UnitOfWork.Customers.GetByEmailAsync(request.Email);
        if (customer == null)
        {
            return Results.Unauthorized();
        }

        return _passwordHasher.VerifyPassword(request.Password, customer.Password)
            ? Results.Ok()
            : Results.Unauthorized();
    }
}
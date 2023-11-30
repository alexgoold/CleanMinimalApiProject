using AutoMapper;
using MediatR;
using Shared.CustomerDtos;

namespace Server.Endpoints.Customers.GetByEmail;

public class GetCustomerByEmailHandler : IRequestHandler<GetCustomerByEmailRequest, IResult>
{
    private readonly IMapper _mapper;

    public GetCustomerByEmailHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<IResult> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
    {
        var customer = await request.UnitOfWork.Customers.GetByEmailAsync(request.Email);
        if (customer == null)
        {
            return Results.NotFound();
        }
        var customerDto = _mapper.Map<CustomerDto>(customer);

        return Results.Ok(customerDto);
    }

}

using AutoMapper;
using MediatR;
using Shared.CustomerDtos;

namespace Server.Endpoints.Customers.GetAll;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersRequest, IResult>
{
	private readonly IMapper _mapper;

	public GetAllCustomersHandler(IMapper mapper)
	{
		_mapper = mapper;
	}

	public async Task<IResult> Handle(GetAllCustomersRequest request, CancellationToken cancellationToken)
	{
		var customers = await request.UnitOfWork.Customers.GetAllAsync();
		var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

		return Results.Ok(customerDtos);
	}

}
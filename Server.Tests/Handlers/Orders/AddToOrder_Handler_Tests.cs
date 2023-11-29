using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.AddToOrder;
using Shared;
using Xunit;

namespace Tests.Handlers.Orders;

public class AddToOrder_Handler_Tests
{
	private readonly AddToOrderHandler _sut;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly AddToOrderRequest _dummyRequest;

	public AddToOrder_Handler_Tests()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<AddToOrderRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new AddToOrderHandler();
	}

	
	
}
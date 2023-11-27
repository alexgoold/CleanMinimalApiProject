using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.GetOrdersByCustomerId;
using Shared.OrderDtos;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Orders;

public class GetOrdersByCustomerId_Handler_Tests
{
	private readonly GetOrdersByCustomerIdHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly GetOrdersByCustomerIdRequest _dummyRequest;

	public GetOrdersByCustomerId_Handler_Tests()
	{
		_fakeMapper = new Fake<IMapper>();
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<GetOrdersByCustomerIdRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new GetOrdersByCustomerIdHandler(_fakeMapper.FakedObject);
	}

	[Fact]
	public async Task Handle_WhenCalled_With_CustomerId_ThatHasOrders_ShouldReturn_Ok_With_AListOf_OrderDtos()
	{
		// Arrange
		var orders = OrderGenerator.Generate3OrdersForOneCustomer();
		_dummyRequest.CustomerId = orders.First().Customer.Id;
		A.CallTo(() => _fakeUnitOfWork.Orders.GetOrdersForCustomerAsync(_dummyRequest.CustomerId)).Returns(orders);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok<IEnumerable<OrderDto>>>();

	}

	[Fact]
	public async Task Handle_WhenCalled_With_CustomerId_ThatHasNoOrders_ShouldReturn_NotFound()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Orders.GetOrdersForCustomerAsync(_dummyRequest.CustomerId)).Returns(A.CollectionOfDummy<Order>(0));

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<NotFound>();
	}

	[Fact]
	public async Task Handle_Calls_Mapper_Once()
	{
		// Arrange
		var orders = OrderGenerator.Generate3OrdersForOneCustomer();
		_dummyRequest.CustomerId = orders.First().Customer.Id;
		A.CallTo(() => _fakeUnitOfWork.Orders.GetOrdersForCustomerAsync(_dummyRequest.CustomerId)).Returns(orders);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeMapper.FakedObject.Map<IEnumerable<OrderDto>>(orders)).MustHaveHappenedOnceExactly();

	}

	[Fact]
	public async Task Handle_Calls_UnitOfWork_Once()
	{
		// Arrange
		var orders = OrderGenerator.Generate3OrdersForOneCustomer();
		_dummyRequest.CustomerId = orders.First().Customer.Id;
		A.CallTo(() => _fakeUnitOfWork.Orders.GetOrdersForCustomerAsync(_dummyRequest.CustomerId)).Returns(orders);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Orders.GetOrdersForCustomerAsync(_dummyRequest.CustomerId)).MustHaveHappenedOnceExactly();

	}

	[Fact]
	public async Task GetOrdersByCustomerIdHandler_Inherits_From_IRequestHandler()
	{
		// Arrange

		// Act

		// Assert
		_sut.Should().BeAssignableTo<IRequestHandler<GetOrdersByCustomerIdRequest, IResult>>();
	}


}
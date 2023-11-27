using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Orders;

public class GetAllOrders_Handler_Tests
{
	private readonly GetAllOrdersHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly GetAllOrdersRequest _dummyRequest;

	public GetAllOrders_Handler_Tests()
	{
		_fakeMapper = new Fake<IMapper>();
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<GetAllOrdersRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new GetAllOrdersHandler(_fakeMapper.FakedObject);
	}

	[Fact]
	public async Task Handle_WhenCalled_WithOrdersInDb_Should_Return_Ok_With_AListOf_OrderDtos()
	{
		// Arrange
		var orders = OrderGenerator.Generate3Orders();
		A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(orders);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok<IEnumerable<OrderDto>>>();
	}


}
using Application.UnitOfWork;
using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.RemoveFromOrder;
using Xunit;

namespace Tests.Handlers.Orders;

public class RemoveFromOrder_Handler_Tests
{
	private readonly RemoveFromOrderHandler _sut;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly RemoveFromOrderRequest _dummyRequest;

	public RemoveFromOrder_Handler_Tests()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<RemoveFromOrderRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new RemoveFromOrderHandler();
	}

	[Fact]
	public async Task Handle_WhenCalled_With_ValidId_ShouldReturn_Ok()
	{
		// Arrange
		var orderId = Guid.NewGuid();
		_dummyRequest.OrderId = orderId;

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}

}
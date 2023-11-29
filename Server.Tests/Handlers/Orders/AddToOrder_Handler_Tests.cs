using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.AddToOrder;
using Tests.Helpers;
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

	[Fact]
	public async Task Handle_WhenCalled_With_ValidCart_ShouldReturn_Ok()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}

	[Fact]
	public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_SaveChangesAsync()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
	}


	[Fact]
	public async Task Handle_WhenCalled_With_Cart_WithInvalidCustomerId_ShouldReturn_NotFound()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;
		A.CallTo(() => _fakeUnitOfWork.Customers.GetAsync(A<Guid>._)).Returns((Customer)null);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<NotFound>();
	}

	[Fact]
	public async Task Handle_WhenCalled_With_Cart_WithAnyInvalidProductId_ShouldReturn_NotFound()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;
		A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(A<Guid>._)).Returns(Task.FromResult<Product?>(null));


		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<NotFound>();
	}

	[Fact]
	public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_GetAsync_For_Each_Product()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(A<Guid>._)).MustHaveHappened(3, Times.Exactly);
	}

	[Fact]
	public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_UpdateAsync_For_Order()
	{
		// Arrange
		var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
		_dummyRequest.Cart = cart;


		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Orders.UpdateAsync(A<Order>._)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task AddToOrderOrderHandler_Inherits_From_IRequestHandler()
	{
		// Arrange

		// Act

		// Assert
		_sut.Should().BeAssignableTo<IRequestHandler<AddToOrderRequest, IResult>>();

	}





}
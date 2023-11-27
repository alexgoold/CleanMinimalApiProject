using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.PlaceOrder;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Orders;

public class PlaceOrder_Handler_Tests
{
    private readonly PlaceOrderHandler _sut;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly PlaceOrderRequest _dummyRequest;

    public PlaceOrder_Handler_Tests()
    {
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<PlaceOrderRequest>();
        _dummyRequest.UnitOfWork = _fakeUnitOfWork;
        _sut = new PlaceOrderHandler();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidCart_ShouldReturn_Ok()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
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
        var cart = CartGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_GetAsync_For_Customer()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_Cart_WithInvalidCustomerId_ShouldReturn_NotFound()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
        cart.CustomerId = Guid.Empty;
        _dummyRequest.Cart = cart;
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAsync(cart.CustomerId)).Returns((Customer?)null);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_Cart_WithAnyInvalidProductId_ShouldReturn_NotFound()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(A<Guid>._)).Returns((Product?)null);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_GetAsync_For_Each_Product()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(A<Guid>._)).MustHaveHappened(3, Times.Exactly);
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidCart_ShouldInvoke_AddAsync_For_Order()
    {
        // Arrange
        var cart = CartGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Orders.AddAsync(A<Order>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task PlaceOrderHandler_Inherits_From_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<PlaceOrderRequest, IResult>>();

    }


}
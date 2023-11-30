using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.RemoveFromOrder;
using Shared.OrderDtos;
using Tests.Helpers;
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
    public async Task Handle_WhenCalled_With_ValidOrder_WithItems_ShouldReturn_Ok()
    {
        // Arrange
        var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        var order = OrderGenerator.GenerateOrder();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns(order);


        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok>();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_IdOfOrder_ThatDoesNotExistInDb_ShouldReturn_NotFound()
    {
        // Arrange
        var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns((Order)null);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidOrder_WithValidItems_ShouldInvoke_UnitOfWork_UpdateAsync()
    {
        // Arrange
        var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        var order = OrderGenerator.GenerateOrder();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns(order);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Orders.UpdateAsync(A<Order>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidOrder_WithItems_ShouldInvoke_UnitOfWork_SaveChangesAsync()
    {
        // Arrange
        var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        var order = OrderGenerator.GenerateOrder();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns(order);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidOrderId_ThatHasNoProducts_ShouldReturn_BadRequest()
    {
        // Arrange
        _dummyRequest.Cart = new CreateOrUpdateOrderDto();
        var order = A.Dummy<Order>();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns(order);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequest>();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_Cart_WithAnyInvalidProductId_ShouldReturn_NotFound()
    {
        // Arrange
        var cart = CreateOrUpdateOrderDtoGenerator.GenerateCartWith3Items();
        _dummyRequest.Cart = cart;
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns(OrderGenerator.GenerateOrder());
        A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(A<Guid>._)).Returns(Task.FromResult<Product?>(null));


        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task RemoveFromOrder_Inherits_From_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<RemoveFromOrderRequest, IResult>>();

    }


}
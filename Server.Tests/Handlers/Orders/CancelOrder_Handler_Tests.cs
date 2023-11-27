using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.CancelOrder;
using Server.Endpoints.Orders.PlaceOrder;
using Xunit;

namespace Tests.Handlers.Orders;

public class CancelOrder_Handler_Tests
{
    private readonly CancelOrderHandler _sut;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly CancelOrderRequest _dummyRequest;

    public CancelOrder_Handler_Tests()
    {
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<CancelOrderRequest>();
        _dummyRequest.UnitOfWork = _fakeUnitOfWork;
        _sut = new CancelOrderHandler();
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

    [Fact]
    public async Task Handle_WhenCalled_With_IdToOrder_ThatDoesNotExistInDb_ShouldReturn_NotFound()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAsync(A<Guid>._)).Returns((Order)null);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task Handle_WhenCalled_ShouldInvoke_UnitOfWork_DeleteAsync()
    {
        // Arrange


        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Orders.DeleteAsync(A<Order>._)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_With_ValidId_ShouldInvoke_UnitOfWork_SaveChangesAsync()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _dummyRequest.OrderId = orderId;

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task CancelOrderHandler_Inherits_From_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<CancelOrderRequest, IResult>>();
    }
}
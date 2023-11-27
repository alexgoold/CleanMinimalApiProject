using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Orders.GetAll;
using Shared.OrderDtos;
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
    public async Task Handle_WhenCalled_WithOrdersInDb_ShouldReturn_Ok_With_AListOf_OrderDtos()
    {
        // Arrange
        var orders = OrderGenerator.Generate3Orders();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(orders);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<OrderDto>>>();
    }

    [Fact]
    public async Task Handle_WhenCalled_WithNoOrdersInDb_ShouldReturn_Ok_With_AnEmptyListOf_OrderDtos()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(new List<Order>());

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<OrderDto>>>();
    }

    [Fact]
    public async Task Handle_WhenCalled_ShouldReturn_Ok_With_ListOf_OrderDtos_ThatMatch_Orders_InDb()
    {
        // Arrange
        var orders = OrderGenerator.Generate3Orders();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(orders);
        var orderDtos = OrderGenerator.Generate3OrderDtos();
        A.CallTo(() => _fakeMapper.FakedObject.Map<IEnumerable<OrderDto>>(orders))
            .Returns(orderDtos);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<OrderDto>>>();
        var okResult = result as Ok<IEnumerable<OrderDto>>;
        okResult?.Value.Should().BeEquivalentTo(orderDtos);
    }

    [Fact]
    public async Task Handle_ShouldInvoke_UnitOfWork_Orders_GetAllAsync_Once()
    {
        // Arrange
        var orders = OrderGenerator.Generate3Orders();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(orders);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ShouldInvoke_Mapper_Map_Once()
    {
        // Arrange
        var orders = OrderGenerator.Generate3Orders();
        A.CallTo(() => _fakeUnitOfWork.Orders.GetAllAsync()).Returns(orders);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeMapper.FakedObject.Map<IEnumerable<OrderDto>>(orders)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetAllOrdersHandler_InheritsFrom_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<GetAllOrdersRequest, IResult>>();
    }






}
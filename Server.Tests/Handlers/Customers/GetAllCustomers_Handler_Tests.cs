using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Server.Endpoints.Customers.GetAll;
using Shared.CustomerDtos;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Customers;

public class GetAllCustomers_Handler_Tests
{
    private readonly GetAllCustomersHandler _sut;
    private readonly Fake<IMapper> _fakeMapper;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly GetAllCustomersRequest _dummyRequest;

    public GetAllCustomers_Handler_Tests()
    {
        _fakeMapper = new Fake<IMapper>();
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<GetAllCustomersRequest>();
        _dummyRequest.UnitOfWork = _fakeUnitOfWork;
        _sut = new GetAllCustomersHandler(_fakeMapper.FakedObject);

    }


    [Fact]
    public async Task Handle_WhenCalled_WithCustomersInDb_Should_Return_Ok_With_AListOf_CustomerDtos()
    {
        // Arrange
        var customers = new List<Customer>()
        {
            CustomerGenerator.GenerateCustomer(),
            CustomerGenerator.GenerateCustomer(),
            CustomerGenerator.GenerateCustomer()
        };
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).Returns(customers);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<CustomerDto>>>();
    }

    [Fact]
    public async Task
        Handle_WhenCalled_WithNoCustomersInDb_Should_Return_Ok_With_AnEmptyListOf_CustomerDtos()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).Returns(new List<Customer>());

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<CustomerDto>>>();
    }

    [Fact]
    public async Task Handle_WhenCalled_Should_Call_UnitOfWork_Customers_GetAllAsync()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).Returns(new List<Customer>());

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_Should_Call_Mapper_Map()
    {
        // Arrange
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).Returns(new List<Customer>());

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeMapper.FakedObject.Map<IEnumerable<CustomerDto>>(A<List<Customer>>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_Should_Return_Ok_With_ListOf_CustomerDtos_That_Match_Customers_InDb()
    {
        // Arrange
        var customers = new List<Customer>()
        {
            CustomerGenerator.GenerateCustomer(),
            CustomerGenerator.GenerateCustomer(),
            CustomerGenerator.GenerateCustomer()
        };
        A.CallTo(() => _fakeUnitOfWork.Customers.GetAllAsync()).Returns(customers);
        var customerDtos = new List<CustomerDto>()
        {
            CustomerGenerator.GenerateCustomerDto(),
            CustomerGenerator.GenerateCustomerDto(),
            CustomerGenerator.GenerateCustomerDto()
        };

        A.CallTo(() => _fakeMapper.FakedObject.Map<IEnumerable<CustomerDto>>(A<List<Customer>>._))
            .Returns(customerDtos);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok<IEnumerable<CustomerDto>>>();
        var okResult = result as Ok<IEnumerable<CustomerDto>>;
        okResult!.Value.Should().BeEquivalentTo(customerDtos);
    }

    [Fact]
    public void GetAllCustomersHandler_Inherits_From_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<GetAllCustomersRequest, IResult>>();
    }

}


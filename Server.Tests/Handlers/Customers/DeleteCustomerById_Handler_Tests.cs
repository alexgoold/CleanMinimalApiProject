using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Customers.Delete;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Customers;

public class DeleteCustomerById_Handler_Tests
{
    private readonly IUnitOfWork _fakekUnitOfWork;
    private readonly DeleteCustomerByIdHandler _sut;
    private readonly DeleteCustomerByIdRequest _dummyRequest;

    public DeleteCustomerById_Handler_Tests()
    {
        _fakekUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<DeleteCustomerByIdRequest>();
        _dummyRequest.UnitOfWork = _fakekUnitOfWork;
        _sut = new DeleteCustomerByIdHandler();
    }

    [Fact]
    public async Task Handle_WhenCalled_WithCustomerInDb_Should_Return_Ok()
    {
        // Arrange
        var customer = new Customer();
        A.CallTo(() => _fakekUnitOfWork.Customers.GetAsync(A<Guid>._)).Returns(customer);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Ok>();
    }

    [Fact]
    public async Task Handle_WhenCalled_WithNoCustomerInDb_Should_Return_NotFound()
    {
        // Arrange
        A.CallTo(() => _fakekUnitOfWork.Customers.GetAsync(A<Guid>._)).Returns((Customer)null);

        // Act
        var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFound>();
    }

    [Fact]
    public async Task Handle_WhenCalled_WithCustomerInDb_Should_Call_UnitOfWork_DeleteAsync()
    {
        // Arrange
        var customer = new Customer();
        A.CallTo(() => _fakekUnitOfWork.Customers.GetAsync(A<Guid>._)).Returns(customer);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakekUnitOfWork.Customers.DeleteAsync(customer)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCalled_WithCustomerInDb_Should_Call_UnitOfWork_SaveChangesAsync()
    {
        // Arrange
        var customer = CustomerGenerator.GenerateCustomer();
        A.CallTo(() => _fakekUnitOfWork.Customers.GetAsync(A<Guid>._)).Returns(customer);

        // Act
        await _sut.Handle(_dummyRequest, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakekUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void DeleteCustomerByIdHandler_Inherits_From_IRequestHandler()
    {
        // Arrange

        // Act

        // Assert
        _sut.Should().BeAssignableTo<IRequestHandler<DeleteCustomerByIdRequest, IResult>>();


    }


}
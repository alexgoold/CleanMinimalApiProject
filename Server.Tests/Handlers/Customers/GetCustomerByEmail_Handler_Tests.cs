using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Customers.GetByEmail;
using Shared.CustomerDtos;
using Xunit;

namespace Tests.Handlers.Customers;

public class GetCustomerByEmail_Handler_Tests
{
    private readonly GetCustomerByEmailHandler _sut;
    private readonly Fake<IMapper> _fakeMapper;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly GetCustomerByEmailRequest _dummyRequest;

    public GetCustomerByEmail_Handler_Tests()
    {
        _fakeMapper = new Fake<IMapper>();
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<GetCustomerByEmailRequest>();
        _dummyRequest.UnitOfWork = _fakeUnitOfWork;
        _sut = new GetCustomerByEmailHandler(_fakeMapper.FakedObject);

    }

    [Fact]
    public void Handle_WhenGiven_ValidCustomerEmail_ShouldReturn_SingleCustomerDto()
    {
        // Arrange
        _dummyRequest.Email = "test@test.com";
        A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(_dummyRequest.Email)).Returns(A.Dummy<Customer>());

        // Act
        var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

        // Assert
        result.Should().BeOfType<Ok<CustomerDto>>();

    }

    [Fact]
    public void Handle_WhenGiven_ValidCustomerEmail_ShouldReturn_SingleCustomerDto_WithMatchingEmail()
    {
        // Arrange
        var email = "test@test.com";
        _dummyRequest.Email = email;

        var customerFromDb = A.Dummy<Customer>();
        customerFromDb.Email = email;

        var customerDto = A.Dummy<CustomerDto>();
        customerDto.Email = email;

        A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(_dummyRequest.Email)).Returns(customerFromDb);
        A.CallTo(() => _fakeMapper.FakedObject.Map<CustomerDto>(customerFromDb)).Returns(customerDto);

        // Act
        var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

        // Assert
        result.Should().BeOfType<Ok<CustomerDto>>();
        result.As<Ok<CustomerDto>>().Value.Email.Should().Be(email);
    }

    [Fact]
    public void Handle_WhenGiven_ValidCustomerEmail_ThatDoesNotMatchCustomerInDatabase_ShouldReturn_NotFound()
    {
        // Arrange
        var email = "test@test.com";
        _dummyRequest.Email = email;

        A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(_dummyRequest.Email)).Returns((Customer?)null);

        // Act
        var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

        // Assert
        result.Should().BeOfType<NotFound>();

    }

}
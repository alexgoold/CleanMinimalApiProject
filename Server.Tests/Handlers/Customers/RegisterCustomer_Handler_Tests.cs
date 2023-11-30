using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Security.HashingStrategy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Customers.Register;
using Shared.CustomerDtos;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Customers;

public class RegisterCustomer_Handler_Tests
{
	private readonly RegisterCustomerHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly Fake<IHashingStrategy> _fakeHasher;
	private readonly Fake<IUnitOfWork> _fakeUnitOfWork;
	private readonly RegisterCustomerRequest _dummyRequest;

	public RegisterCustomer_Handler_Tests()
	{
		_fakeMapper = new Fake<IMapper>();
		_fakeHasher = new Fake<IHashingStrategy>();
		_sut = new RegisterCustomerHandler(_fakeMapper.FakedObject, _fakeHasher.FakedObject);
		_dummyRequest = A.Dummy<RegisterCustomerRequest>();
		_dummyRequest.Customer = A.Dummy<CustomerDto>();
		_dummyRequest.UnitOfWork = A.Fake<IUnitOfWork>();
		A.CallTo(() => _fakeMapper.FakedObject.Map<CustomerDto>(_dummyRequest.Customer)).Returns(CustomerGenerator.GenerateCustomerDto());

	}

	[Fact]
	public async Task Handle_WhenGiven_ValidCustomer_ShouldReturn_OkResult()
	{
		// Arrange

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}
	
	[Fact]
	public async Task Handle_WhenGiven_ValidCustomer_ShouldCall_HashPassword_Once()
	{
		// Arrange

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeHasher.FakedObject.HashPassword(_dummyRequest.Customer.Password)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Handle_WhenGiven_ValidCustomer_ShouldCall_AddAsync_Once()
	{
		// Arrange

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _dummyRequest.UnitOfWork.Customers.AddAsync(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Handle_WhenGiven_ValidCustomer_ShouldCall_SaveChangesAsync_Once()
	{
		// Arrange

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _dummyRequest.UnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task Handle_WhenGiven_ValidCustomer_ShouldCall_Map_Once()
	{
		// Arrange

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeMapper.FakedObject.Map<Customer>(_dummyRequest.Customer)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public void RegisterCustomerHandler_Inherits_IRequestHandler()
	{
		// Arrange

		// Act

		// Assert
		_sut.Should().BeAssignableTo<IRequestHandler<RegisterCustomerRequest, IResult>>();
	}
	
}
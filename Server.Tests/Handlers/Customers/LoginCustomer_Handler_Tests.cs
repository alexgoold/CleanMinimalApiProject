using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Security.HashingStrategy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Customers.Login;
using Xunit;

namespace Tests.Handlers.Customers;

public class LoginCustomer_Handler_Tests
{
	private readonly LoginCustomerHandler _sut;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly LoginCustomerRequest _dummyRequest;
	private readonly IHashingStrategy _fakeHashingStrategy;

	public LoginCustomer_Handler_Tests()
	{
		_fakeHashingStrategy = A.Fake<IHashingStrategy>();
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<LoginCustomerRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new LoginCustomerHandler(_fakeHashingStrategy);

	}

	[Fact]
	public async Task Handle_WhenGivenValid_Email_And_CorrectPassword_ShouldReturn_Ok()
	{
		// Arrange
		_dummyRequest.Email = "fake@email.com";
		_dummyRequest.Password = "password";
		
		A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(_dummyRequest.Email)).Returns(A.Dummy<Domain.Customer>());
		A.CallTo(() => _fakeHashingStrategy.VerifyPassword(A<string>._, A<string>._)).Returns(true);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}

	[Fact]
	public async Task Handle_WhenGiven_Email_And_IncorrectPassword_ShouldReturn_Unauthorized()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(A<string>._)).Returns(A.Dummy<Domain.Customer>());
		A.CallTo(() => _fakeHashingStrategy.VerifyPassword(A<string>._, A<string>._)).Returns(false);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<UnauthorizedHttpResult>();

	}

	[Fact]
	public async Task Handle_WhenGiven_InvalidEmail_ShouldReturn_Unauthorized()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(A<string>._)).Returns((Customer)null);
		A.CallTo(() => _fakeHashingStrategy.VerifyPassword(A<string>._, A<string>._)).Returns(true);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<UnauthorizedHttpResult>();
	}

	[Fact]
	public async Task Handle_WhenGiven_ValidEmail_And_CorrectPassword_ShouldCall_GetByEmailAsync_Once()
	{
		// Arrange
		
		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Customers.GetByEmailAsync(_dummyRequest.Email)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public void LoginCustomerRequest_Inherits_IRequestHandler()
	{
		// Arrange
		
		// Act
		
		// Assert
		typeof(IRequestHandler<LoginCustomerRequest, IResult>).IsAssignableFrom(typeof(LoginCustomerHandler)).Should().BeTrue();
	}

	
}
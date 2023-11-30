using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using Xunit;

namespace Tests.Handlers.Customers;

public class LoginCustomer_Handler_Tests
{
	private readonly LoginCustomerHandler _sut;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly LoginCustomerRequest _dummyRequest;

	public LoginCustomer_Handler_Tests()
	{
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<LoginCustomerRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new LoginCustomerHandler(_fakeMapper.FakedObject);

	}

	[Fact]
	public void Handle_WhenGivenValid_Email_And_CorrectPassword_ShouldReturn_Ok()
	{

	}

	
}
using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Products.Add;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Products;

public class AddProduct_Handler_Tests
{
	private readonly AddProductHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly AddProductRequest _dummyRequest;

	public AddProduct_Handler_Tests()
	{
		_fakeMapper = new Fake<IMapper>();
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<AddProductRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new AddProductHandler(_fakeMapper.FakedObject);
	}

	[Fact]
	public async Task Handle_WhenCalled_WithValidProductDto_Should_Return_Ok()
	{
		// Arrange
		var productDto = ProductGenerator.GenerateProductDto();
		_dummyRequest.ProductDto = productDto;

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}


}
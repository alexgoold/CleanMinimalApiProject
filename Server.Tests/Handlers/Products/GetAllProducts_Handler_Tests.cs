using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Server.Endpoints.Products.GetAll;
using Shared.ProductsDtos;
using Tests.Helpers;
using Xunit;

namespace Tests.Handlers.Products;

public class GetAllProducts_Handler_Tests
{
	private readonly GetAllProductsHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly IUnitOfWork _fakeUnitOfWork;
	private readonly GetAllProductsRequest _dummyRequest;

	public GetAllProducts_Handler_Tests()
	{
		_fakeMapper = new Fake<IMapper>();
		_fakeUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<GetAllProductsRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new GetAllProductsHandler(_fakeMapper.FakedObject);

	}

	[Fact]
	public async Task GetAllProductHandler_WhenCalled_WithProductsInDb_Should_Return_AListOf_ProductDtos()
	{
		// Arrange
		var products = ProductGenerator.GenerateListOf3Products();
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).Returns(products);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<List<ProductDto>>();

	}
}
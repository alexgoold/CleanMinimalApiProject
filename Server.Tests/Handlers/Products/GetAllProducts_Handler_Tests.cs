using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
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
	public async Task GetAllProductHandler_WhenCalled_WithProductsInDb_Should_Return_Ok_With_AListOf_ProductDtos()
	{
		// Arrange
		var products = ProductGenerator.GenerateListOf3Products();
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).Returns(products);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok<List<ProductDto>>>();
	}

	[Fact]
	public async Task
		GetAllProductHandler_WhenCalled_WithNoProductsInDb_Should_Return_Ok_With_AnEmptyListOf_ProductDtos()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).Returns(new List<Product>());

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok<List<ProductDto>>>();
	}

	[Fact]
	public async Task GetAllProductHandler_WhenCalled_Should_Call_UnitOfWork_Products_GetAllAsync()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).Returns(new List<Product>());

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task GetAllProductHandler_WhenCalled_Should_Call_Mapper_Map()
	{
		// Arrange
		A.CallTo(() => _fakeUnitOfWork.Products.GetAllAsync()).Returns(new List<Product>());

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeMapper.FakedObject.Map<List<ProductDto>>(A<List<Product>>._))
			.MustHaveHappenedOnceExactly();
	}


}
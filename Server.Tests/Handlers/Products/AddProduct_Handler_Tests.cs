using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		A.CallTo(() => _fakeMapper.FakedObject.Map<Product>(productDto)).Returns(ProductGenerator.GenerateProduct());

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithValidProductDto_Should_Call_Mapper()
	{
		// Arrange
		var productDto = ProductGenerator.GenerateProductDto();
		_dummyRequest.ProductDto = productDto;

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeMapper.FakedObject.Map<Product>(productDto)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithValidProductDto_Should_Call_UnitOfWork()
	{
		// Arrange
		var productDto = ProductGenerator.GenerateProductDto();
		_dummyRequest.ProductDto = productDto;
		var validProduct = ProductGenerator.GenerateProduct();

		A.CallTo(() => _fakeMapper.FakedObject.Map<Product>(productDto)).Returns(validProduct);

		// Act
		await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakeUnitOfWork.Products.AddAsync(A<Product>._)).MustHaveHappened();
	}

	[Fact]
	public async Task AddProductHandler_ShouldInherit_From_IRequestHandler()
	{
		// Arrange

		// Act

		// Assert
		_sut.Should().BeAssignableTo<IRequestHandler<AddProductRequest, IResult>>();
	}

	


}
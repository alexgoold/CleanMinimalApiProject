using Application.UnitOfWork;
using AutoMapper;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Products.Get;
using Shared.ProductsDtos;
using Xunit;

namespace Tests.Handlers.Products;

public class GetProduct_Handler_Tests
{
    private readonly GetProductHandler _sut;
	private readonly Fake<IMapper> _fakeMapper;
	private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly GetProductRequest _dummyRequest;

    public GetProduct_Handler_Tests()
    {
        _fakeMapper = new Fake<IMapper>();
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _dummyRequest = A.Dummy<GetProductRequest>();
        _dummyRequest.UnitOfWork = _fakeUnitOfWork;
        _sut = new GetProductHandler(_fakeMapper.FakedObject);
        
    }

    [Fact]
    public void Handle_WhenGiven_ValidProductId_ShouldReturn_SingleProductDto()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _dummyRequest.ProductId = guid;
		A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(_dummyRequest.ProductId)).Returns(A.Dummy<Product>());

        // Act
        var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

        // Assert
        result.Should().BeOfType<Ok<ProductDto>>();
        
    }

    [Fact]
    public void Handle_WhenGiven_ValidProductId_ShouldReturn_SingleProductDto_WithMatchingId()
    {
		// Arrange
		var guid = Guid.NewGuid();
		_dummyRequest.ProductId = guid;

        var productFromDb = A.Dummy<Product>();
        productFromDb.Id = guid;

        var productDto = A.Dummy<ProductDto>();
        productDto.Id = guid;

		A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(_dummyRequest.ProductId)).Returns(productFromDb);
        A.CallTo(() => _fakeMapper.FakedObject.Map<ProductDto>(productFromDb)).Returns(productDto);

		// Act
		var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

		// Assert
        result.Should().BeOfType<Ok<ProductDto>>();
        result.As<Ok<ProductDto>>().Value.Id.Should().Be(guid);
    }

    [Fact]
    public void Handle_WhenGiven_ValidProductId_ThatDoesNotMatchProductInDatabase_ShouldReturn_NotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _dummyRequest.ProductId = guid;
		A.CallTo(() => _fakeUnitOfWork.Products.GetAsync(_dummyRequest.ProductId)).Returns((Product?)null);

        // Act
		var result = _sut.Handle(_dummyRequest, CancellationToken.None).Result;

		// Assert
		result.Should().BeOfType<NotFound>();
        
    }

 }
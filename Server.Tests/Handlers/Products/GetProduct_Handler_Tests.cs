using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Products.Get;
using Xunit;

namespace Tests.Handlers.Products;

public class GetProduct_Handler_Tests
{
    private GetProductHandler _sut;

    public GetProduct_Handler_Tests()
    {
        _sut = new GetProductHandler();
    }

    [Fact]
    public async Task Handle_WhenGiven_ValidProductId_ShouldReturn_SingleProduct()
    {
        // Arrange
        var dummyRequest = A.Dummy<GetProductRequest>();
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var guid = Guid.NewGuid();
        dummyRequest.UnitOfWork = fakeUnitOfWork;
        dummyRequest.ProductId = guid;
        A.CallTo(() => fakeUnitOfWork.Products.GetAsync(dummyRequest.ProductId)).Returns(A.Dummy<Domain.Product>());

        // Act
        var result = _sut.Handle(dummyRequest, CancellationToken.None).Result;

        // Assert
        result.Should().BeOfType<Ok<Product>>();
        
    }

    [Fact]
    public async Task Handle_WhenGiven_ValidProductId_ShouldReturn_SingleProduct_WithMatchingId()
    {
		// Arrange
		var dummyRequest = A.Dummy<GetProductRequest>();
		var fakeUnitOfWork = A.Fake<IUnitOfWork>();
		var guid = Guid.NewGuid();
		dummyRequest.UnitOfWork = fakeUnitOfWork;
		dummyRequest.ProductId = guid;
		A.CallTo(() => fakeUnitOfWork.Products.GetAsync(dummyRequest.ProductId)).Returns(A.Dummy<Domain.Product>());

		// Act
		var result = _sut.Handle(dummyRequest, CancellationToken.None).Result;

		// Assert
		result.Should().BeOfType<Ok<Product>>().Which.Value.Id.Equals(guid);
    }
}
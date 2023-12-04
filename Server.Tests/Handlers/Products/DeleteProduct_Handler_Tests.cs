using Application.UnitOfWork;
using Domain;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Endpoints.Products.Delete;
using Xunit;

namespace Tests.Handlers.Products;

public class DeleteProduct_Handler_Tests
{
	private DeleteProductHandler _sut;
	private DeleteProductRequest _dummyRequest;
	private IUnitOfWork _fakekUnitOfWork;

	public DeleteProduct_Handler_Tests()
	{
		_fakekUnitOfWork = A.Fake<IUnitOfWork>();
		_dummyRequest = A.Dummy<DeleteProductRequest>();
		_dummyRequest.UnitOfWork = _fakekUnitOfWork;
		_sut = new DeleteProductHandler();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithProductInDb_Should_Return_Ok()
	{
		// Arrange
		var product = new Product();
		A.CallTo(() => _fakekUnitOfWork.Products.GetAsync(A<Guid>._)).Returns(product);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<Ok>();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithNoProductInDb_Should_Return_NotFound()
	{
		// Arrange
		A.CallTo(() => _fakekUnitOfWork.Products.GetAsync(A<Guid>._)).Returns((Product)null);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		result.Should().BeOfType<NotFound>();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithProductInDb_Should_Call_UnitOfWork_DeleteAsync()
	{
		// Arrange
		var product = new Product();
		A.CallTo(() => _fakekUnitOfWork.Products.GetAsync(A<Guid>._)).Returns(product);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakekUnitOfWork.Products.DeleteAsync(A<Product>._)).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task Handle_WhenCalled_WithProductInDb_Should_Call_UnitOfWork_SaveChangesAsync()
	{
		// Arrange
		var product = new Product();
		A.CallTo(() => _fakekUnitOfWork.Products.GetAsync(A<Guid>._)).Returns(product);

		// Act
		var result = await _sut.Handle(_dummyRequest, CancellationToken.None);

		// Assert
		A.CallTo(() => _fakekUnitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
	}

	[Fact]
	public void DeleteProductHandler_Inherits_From_IRequestHandler()
	{
		// Arrange

		// ACt

		// Assert
		_sut.Should().BeAssignableTo<IRequestHandler<DeleteProductRequest, IResult>>();
	}


	
}
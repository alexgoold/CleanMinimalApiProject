using Application.UnitOfWork;
using FakeItEasy;
using FluentAssertions;
using Server.Mediator.Products.Get;
using Xunit;

namespace Tests.Handlers.Product;

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
		var request = A.Dummy<GetProductRequest>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		A.CallTo(() => unitOfWork.Products.GetAsync(request.ProductId)).Returns(A.Dummy<Domain.Product>());

		// Act
		var result = await _sut.Handle(request, CancellationToken.None);

		// Assert
		result.Should().NotBeNull();
	}
}
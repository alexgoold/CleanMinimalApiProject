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
        var dummyRequest = A.Dummy<GetProductRequest>();
        var fakeUnitOfWork = A.Fake<IUnitOfWork>();
        var guid = Guid.NewGuid();
        dummyRequest.UnitOfWork = fakeUnitOfWork;
        dummyRequest.ProductId = guid;
        A.CallTo(() => fakeUnitOfWork.Products.GetAsync(dummyRequest.ProductId)).Returns(A.Dummy<Domain.Product>());

        // Act
        var result = await _sut.Handle(dummyRequest, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}
using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using Server.Endpoints.Products.Get;

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
		_dummyRequest = A.Dummy<GetProductRequest>();
		_dummyRequest.UnitOfWork = _fakeUnitOfWork;
		_sut = new GetProductHandler(_fakeMapper.FakedObject);

	}
}
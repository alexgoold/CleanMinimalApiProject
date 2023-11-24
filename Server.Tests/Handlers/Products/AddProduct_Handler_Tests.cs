using Application.UnitOfWork;
using AutoMapper;
using FakeItEasy;
using Server.Endpoints.Products.Get;

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

}
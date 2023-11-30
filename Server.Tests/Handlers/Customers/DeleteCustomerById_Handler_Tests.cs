using Application.UnitOfWork;
using FakeItEasy;
using Server.Endpoints.Customers.Delete;
using Xunit;

namespace Tests.Handlers.Customers;

public class DeleteCustomerById_Handler_Tests
{
    private readonly IUnitOfWork _fakekUnitOfWork;
    private readonly DeleteCustomerByIdHandler _sut;
    private readonly DeleteCustomerByIdRequest _request;

    public DeleteCustomerById_Handler_Tests()
    {
        _fakekUnitOfWork = A.Fake<IUnitOfWork>();
        _request = A.Fake<DeleteCustomerByIdRequest>();
        _sut = new DeleteCustomerByIdHandler();
    }

}
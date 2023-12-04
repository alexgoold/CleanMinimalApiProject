using MediatR;

namespace Server.Endpoints.Customers.Delete;

public class DeleteCustomerByIdHandler : IRequestHandler<DeleteCustomerByIdRequest, IResult>
{
    public async Task<IResult> Handle(DeleteCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customerToDelete = await request.UnitOfWork.Customers.GetAsync(request.Id);
        if (customerToDelete == null)
        {
            return Results.NotFound();
        }

        await request.UnitOfWork.Customers.DeleteAsync(customerToDelete);
        await request.UnitOfWork.SaveChangesAsync();

        return Results.Ok();
    }
}
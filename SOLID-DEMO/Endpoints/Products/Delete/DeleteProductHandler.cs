using MediatR;

namespace Server.Endpoints.Products.Delete;

public class DeleteProductHandler :IRequestHandler<DeleteProductRequest, IResult>
{
	public async Task<IResult> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
	{
		var productToDelete = await request.UnitOfWork.Products.GetAsync(request.Id);
		if (productToDelete == null)
		{
			return Results.NotFound();
		}

		await request.UnitOfWork.Products.DeleteAsync(productToDelete);
		await request.UnitOfWork.SaveChangesAsync();

		return Results.Ok();
	}
}
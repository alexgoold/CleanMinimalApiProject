using MediatR;

namespace Server.Mediator.Products.Get
{
	public class GetProductHandler : IRequestHandler<GetProductRequest, IResult>
	{
		public async Task<IResult> Handle(GetProductRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

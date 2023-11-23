using MediatR;

namespace Server.Mediator;

public interface IHttpRequest : IRequest<IResult>
{

}
using MediatR;

namespace Server.Endpoints;

public interface IHttpRequest : IRequest<IResult>
{

}
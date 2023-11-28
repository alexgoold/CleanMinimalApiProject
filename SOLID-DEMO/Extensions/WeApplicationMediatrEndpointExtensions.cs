using MediatR;
using Server.Endpoints;

namespace Server.Extensions
{
    public static class WeApplicationMediatrEndpointExtensions
    {
        public static WebApplication MediateGet<TRequest>(this WebApplication app, string url) where TRequest : IHttpRequest
        {
            app.MapGet(url,
                async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
            return app;
        }
        public static WebApplication MediatePost<TRequest>(this WebApplication app, string url) where TRequest : IHttpRequest
        {
            app.MapPost(url,
                async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
            return app;
        }
        public static WebApplication MediateDelete<TRequest>(this WebApplication app, string url) where TRequest : IHttpRequest
        {
            app.MapDelete(url,
                async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
            return app;
        }
        public static WebApplication MediatePut<TRequest>(this WebApplication app, string url) where TRequest : IHttpRequest
        {
            app.MapPut(url,
                async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
            return app;
        }
    }
}

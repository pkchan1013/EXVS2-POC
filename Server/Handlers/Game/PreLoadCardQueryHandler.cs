using MediatR;
using nue.protocol.exvs;

namespace Server.Handlers.Game;

public record PreLoadCardQuery(Request Request) : IRequest<Response>;

public class PreLoadCardQueryHandler : IRequestHandler<PreLoadCardQuery, Response>
{
    public Task<Response> Handle(PreLoadCardQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;
        var response = new Response
        {
            Type = request.Type,
            RequestId = request.RequestId,
            Error = Error.Success
        };
        response.pre_load_card = new Response.PreLoadCard
        {
            SessionId = "1",
            IsNewCard = true,
        };
        return Task.FromResult(response);
    }
}
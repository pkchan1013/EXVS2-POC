using MediatR;
using Newtonsoft.Json;
using nue.protocol.exvs;
using Serilog;

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

        Log.Information(Directory.GetCurrentDirectory());
        String readStr = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "preloadcard.json"));
        response.pre_load_card = JsonConvert.DeserializeObject<Response.PreLoadCard>(readStr);
        
        // String jsonStr = JsonConvert.SerializeObject(response.pre_load_card);
        // Log.Information("JSON String");
        // Log.Information(jsonStr);

        return Task.FromResult(response);
    }
}
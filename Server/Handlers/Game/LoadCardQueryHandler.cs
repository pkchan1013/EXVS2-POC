using MediatR;
using nue.protocol.exvs;

namespace Server.Handlers.Game;

public record LoadCardQuery(Request Request) : IRequest<Response>;

public class LoadCardQueryHandler : IRequestHandler<LoadCardQuery, Response>
{
    public Task<Response> Handle(LoadCardQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;

        var response = new Response
        {
            Type = request.Type,
            RequestId = request.RequestId,
            Error = Error.Success,
            load_card = new Response.LoadCard
            {
                pilot_data_group = new Response.LoadCard.PilotDataGroup
                {
                    TotalTriadScore = 0,
                    TotalTriadScenePlayNum = 0,
                    TotalTriadWantedDefeatNum = 0,
                    Training = new Response.LoadCard.PilotDataGroup.TrainingSettingGroup
                    {
                        MstMobileSuitId = 1,
                        BurstType = 1,
                        CpuLevel = 1,
                        ExBurstGauge = 0,
                        DamageDisplay = true,
                        CpuAutoGuard = false,
                        CommandGuideDisplay = true
                    }
                },
            }
        };
        return Task.FromResult(response);
    }
}
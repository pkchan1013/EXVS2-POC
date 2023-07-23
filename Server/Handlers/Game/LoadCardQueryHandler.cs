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
                    },
                    MsSkills =
                    {
                        {
                            new Response.LoadCard.PilotDataGroup.MSSkillGroup()
                                {
                                    MstMobileSuitId = 1,
                                    MsUsedNum = 5000,
                                    CostumeId = 1u,
                                    TriadBuddyPoint = 0
                                }
                        },
                        {
                            new Response.LoadCard.PilotDataGroup.MSSkillGroup()
                            {
                                MstMobileSuitId = 123, // Gundam Seed Destiny, Strike Freedom Gundam, Kira Yamato (CE73)
                                MsUsedNum = 5000,
                                CostumeId = 1u, // 1u for Casual CE73 Kira Yamato, 0u for Normal Suit
                                TriadBuddyPoint = 0
                            }
                        },
                        {
                            new Response.LoadCard.PilotDataGroup.MSSkillGroup()
                            {
                                MstMobileSuitId = 124, // Gundam Seed Destiny, Infinite Justice Gundam, Athrun Zala (CE73)
                                MsUsedNum = 5000,
                                CostumeId = 1u, // 1u for Orb Athrun Zala, 0u for Normal Suit
                                TriadBuddyPoint = 0
                            }
                        }
                    }
                },
            }
        };
        return Task.FromResult(response);
    }
}
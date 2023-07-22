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
            IsNewCard = false,
            load_player = new Response.PreLoadCard.LoadPlayer
            {
                PilotId = 1,
                TotalWin = 0,
                TotalLose = 0,
                EchelonId = 1,
                EchelonExp = 0,
                SEchelonFlag = false,
                SEchelonMissionFlag = false,
                SEchelonProgress = 0,
                SCaptainFlag = false,
                SBrigadierFlag = false,
                VsmAfterRankUp = 0,
                ShuffleWin = 0,
                ShuffleLose = 0,
                TeamWin = 0,
                TeamLose = 0,
                RankSoloWin = 0,
                RankSoloLose = 0,
                RankTeamWin = 0,
                RankTeamLose = 0,
                CasualSoloWin = 0,
                CasualSoloLose = 0,
                CasualTeamWin = 0,
                CasualTeamLose = 0,
                RankIdSolo = 1,
                RankIdTeam = 1,
                AchievedExNumSolo = 0,
                AchievedExNumTeam = 0,
                AchievedExxNumSolo = 0,
                AchievedExxNumTeam = 0
            },
            AcidError = AcidError.AcidNoUse,
            User = new Response.PreLoadCard.MobileUserGroup
            {
                UserId = 1,
                PlayerName = "测试-テスト",
                OpenRecord = 1,
                OpenEchelon = 1,
                OpenSkillpoint = true,
                KeyconfigNumber = 1,
                Gp = 1
            }
        };
        return Task.FromResult(response);
    }
}
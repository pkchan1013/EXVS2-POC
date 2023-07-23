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
                Gp = 1,
                GuestNavs =
                {
                    {
                        new Response.PreLoadCard.MobileUserGroup.GuestNavGroup
                        {
                            GuestNavSettingFlag = true, // Enable or not
                            GuestNavId = 29u, // Romary Stone (AGE)
                            GuestNavCostume = 0u, // 1u = Casual, 0u = Default
                            GuestNavFamiliarity = 50,
                            GuestNavRemains = 99999,
                            NewCostumeFlag = false,
                            BattleNavSettingFlag = true,
                            BattleNavRemains = 99999
                        }
                    }
                },
                FavoriteMobileSuits =
                {
                    new Response.PreLoadCard.MobileUserGroup.FavoriteMSGroup {
                        MstMobileSuitId = 123, // Strike Freedom
                        MsUsedNum = 5000,
                        OpenSkillpoint = true,
                        GaugeDesignId = 0,
                        BgmPlayMethod = 0,
                        BattleNavId = 29u, // Must Exist in GuestNavs
                        BurstType = 3, // 1 = Fighting, 2 = Shooting, 3 = Mobile
                        DefaultTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                        RankMatchTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                        TriadTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                    },
                    new Response.PreLoadCard.MobileUserGroup.FavoriteMSGroup {
                        MstMobileSuitId = 124, // Infinite Justice
                        MsUsedNum = 5000,
                        OpenSkillpoint = true,
                        GaugeDesignId = 0,
                        BgmPlayMethod = 0,
                        BattleNavId = 29u, // Must Exist in GuestNavs
                        BurstType = 3, // 1 = Fighting, 2 = Shooting, 3 = Mobile
                        DefaultTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                        RankMatchTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                        TriadTitleCustomize = new TitleCustomize()
                        {
                            TitleTextId = 0,
                            TitleEffectId = 0,
                            TitleOrnamentId = 0,
                            TitleBackgroundPartsId = 0
                        },
                    }
                }
            }
        };
        return Task.FromResult(response);
    }
}
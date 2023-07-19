using Microsoft.AspNetCore.Mvc;
using nue.protocol.exvs;
using Swan.Formatters;

namespace Server.Controllers;

public class GameController : BaseController<GameController>
{
    [Route("")]
    [HttpPost]
    [Produces("application/protobuf")]
    public IActionResult Game([FromBody] Request request)
    {
        Logger.LogInformation("Request is {Request}", request.Stringify());
        var response = new Response
        {
            Type = MethodType.MthdRegisterPcb,
            RequestId = request.RequestId,
            Error = Error.Success
        };
        switch (request.Type)
        {
            case MethodType.MthdRegisterPcb:
                response.register_pcb = new Response.RegisterPcb
                {
                    Ipv4Flag = true,
                    NextMaintenanceStartAt = 2005364002,
                    NextMaintenanceEndAt = 2005364004,
                    SramClear = true,
                    LmIpAddresses = {"192.168.50.239"},
                    ServerInfoes =
                    {
                        new Response.RegisterPcb.ServerInfo
                        {
                            ServerType = ServerType.SrvMatch,
                            Uri = "vsapi.taiko-p.jp",
                            Port = 12345
                        }
                    },
                    core_dump_res =
                    {
                        new Response.RegisterPcb.CoreDumpRes
                        {
                            FileName = "test",
                            Url = "http://vsapi.taiko-p.jp/test"
                        }
                    }
                };
                Logger.LogInformation("Response: {Response}", response.Stringify());
                return Ok(response);
            case MethodType.MthdRegisterPcbAck:
                response.register_pcb_ack = new Response.RegisterPcbAck();
                return Ok(response);
            case MethodType.MthdSaveInsideData:
                response.save_inside_data = new Response.SaveInsideData();
                return Ok(response);
            case MethodType.MthdPing:
                response.ping = new Response.Ping
                {
                    GameServer = true,
                    AcidServer = false,
                    MatchmakingServer = false,
                    ResponseAt = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                return Ok(response);
            case MethodType.MthdCheckTime:
                response.check_time = new Response.CheckTime
                {
                    At = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                return Ok(response);
            case MethodType.MthdCheckResourceData:
                response.check_resource_data = new Response.CheckResourceData();
                return Ok(response);
            case MethodType.MthdLoadGameData:
                response.load_game_data = new Response.LoadGameData
                {
                    on_vs_info = new Response.LoadGameData.OnVsInfo
                    {
                        RuleTimeLimitRank = 20,
                        RuleTimeLimitCasual = 20,
                        RuleTimeLimitEx = 20,
                        RuleDamageLevelTeam = 1,
                        RuleDamageLevelShuffle = 1
                    },
                    EchelonTables =
                    {
                        new Response.LoadGameData.EchelonTable
                        {
                            EchelonId = 1
                        }
                    },
                    ReplayUnderEchelonId = 1,
                    AdvancedReplayUnderEchelonId = 1,
                    TrainingTimeLimit = 20,
                    PcoinLoseExpRelaxationRate = 1,
                    PcoinTeamGpIncreaseRate = 1,
                    NewcardCampaignFlag = true,
                    CasualBaseWinPoint = 1,
                    CasualBaseLosePoint = 1,
                    OfflineBaseLosePoint = 1,
                    OfflineBaseWinPoint = 1,
                    CasualLoseResultBonus = 1,
                    OfflineLoseResultBonus = 1,
                    ReplayUnderRankId = 1,
                    AdvancedReplayUnderRankId = 1,
                    WantedDownLevel = 1,
                    WantedAttackLevel = 1,
                    WantedPsAttackLevel = 1,
                    WantedPsDefenceLevel = 1,
                    LoadGameDataVer = 1,
                    score_battle_point = new Response.LoadGameData.ScoreBattlePoint
                    {
                        ScoreBattle1500Point = 1,
                        ScoreBattle2000Point = 1,
                        ScoreBattle2500Point = 1,
                        ScoreBattle3000Point = 1
                    },
                    attack_score_setting = new Response.LoadGameData.AttackScoreSetting
                    {
                        DownScoreTimes = 1,
                        Last30CountScoreTimes = 1,
                        NoAttackDecreaseScore = 1
                    }
                };
                return Ok(response);
            case MethodType.MthdLoadRankMatch:
            case MethodType.MthdPreLoadCard:
            case MethodType.MthdLoadCard:
            case MethodType.MthdRegisterCard:
            case MethodType.MthdSaveVsmResult:
            case MethodType.MthdSaveVsmOnResult:
            case MethodType.MthdSaveVscResult:
            case MethodType.MthdSaveVstResult:
            case MethodType.MthdSaveVsmToResult:
            case MethodType.MthdSaveCharge:
            case MethodType.MthdSaveMedal:
            case MethodType.MthdSaveBattleLog:
            case MethodType.MthdSaveBattleLogOn:
            case MethodType.MthdSaveUserPlayResearchData:
            case MethodType.MthdSaveResultCapture:
            case MethodType.MthdSaveFailedBattleLogOn:
            case MethodType.MthdCheckCommunication:
            case MethodType.MthdLoadAccessCode:
            case MethodType.MthdUsePCoinTicket:
            case MethodType.MthdLoadSpotInfo:
            case MethodType.MthdLoadRanking:
            case MethodType.MthdCheckTelop:
            case MethodType.MthdLoadTelop:
            case MethodType.MthdCheckMovieRelease:
            case MethodType.MthdLoadSpotUrl:
            case MethodType.MthdLoadReplayCard:
            case MethodType.MthdPreSaveReplay:
            case MethodType.MthdLoadMeetingCard:
            case MethodType.MthdSaveTournamentResult:
            case MethodType.MthdLoadBlackList:
            case MethodType.MthdStartTournament:
            case MethodType.MthdSaveLog:
            default:
                return NotFound();
        }
    }

    [Route("")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello");
    }
}
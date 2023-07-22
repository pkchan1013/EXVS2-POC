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
            Type = request.Type,
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
                            Uri = "vsapi.taiko-p.jp/match",
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
                break;
            case MethodType.MthdRegisterPcbAck:
                response.register_pcb_ack = new Response.RegisterPcbAck();
                break;
            case MethodType.MthdSaveInsideData:
                response.save_inside_data = new Response.SaveInsideData();
                break;
            case MethodType.MthdPing:
                response.ping = new Response.Ping
                {
                    GameServer = true,
                    AcidServer = false,
                    MatchmakingServer = false,
                    ResponseAt = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                break;
            case MethodType.MthdCheckTime:
                response.check_time = new Response.CheckTime
                {
                    At = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                break;
            case MethodType.MthdCheckResourceData:
                response.check_resource_data = new Response.CheckResourceData();
                break;
            case MethodType.MthdLoadGameData:
                var allMsIds = Enumerable.Range(1, 400).Select(i => (uint)i).ToArray();
                response.load_game_data = new Response.LoadGameData
                {
                    ReleaseMsIds = allMsIds, // ms unlock ids
                    NewMsIds = Array.Empty<uint>(), // responsible for showing ms under "new" series
                    DisplayableMsIds = allMsIds, // responsible for triad battle ai enemy units
                    ReleaseGuestNavIds = allMsIds, // responsible for triad battle ai partners
                    ReleaseGameRules = new []{ 1u, 2u },
                    UpdateMsIds = Array.Empty<uint>(), // add a 'update' tag to ms
                    on_vs_info = new Response.LoadGameData.OnVsInfo
                    {
                        RuleTimeLimitRank = 230, // player match time in seconds
                        RuleTimeLimitCasual = 230, 
                        RuleTimeLimitEx = 230,
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
                    ReleaseCpuScenes = new []{1u,2u},
                    MstMobileSuitIds = new []{1u,2u},
                    OfflineWinEchelonNums =
                    {
                        new Response.LoadGameData.OfflineEchelon
                        {
                            Id = 1u,
                            LowerThreshold = 1,
                            UpperThreshold = 100,
                            Point = 1
                        }
                    },
                    OfflineLoseEchelonNums =
                    {
                        new Response.LoadGameData.OfflineEchelon
                        {
                            Id = 1,
                            LowerThreshold = 1,
                            UpperThreshold = 100,
                            Point = 1
                        }
                    },
                    
                    ReplayUnderEchelonId = 1,
                    AdvancedReplayUnderEchelonId = 1,
                    TrainingTimeLimit = 12, // training mode's countdown time in minutes
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
                    WantedDownLevel = 0, // this will cause enemy to 1 hit down if set to 1
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
                response.load_game_data.ReleaseCpuCourses.AddRange(Enumerable.Range(1, 100).Select(i => 
                    new Response.LoadGameData.ReleaseCpuCourse
                    {
                        CourseId = (uint)i,
                        OpenedAt = (ulong)(DateTimeOffset.Now - TimeSpan.FromDays(10)).ToUnixTimeSeconds()
                    }));
                break;
            case MethodType.MthdLoadRankMatch:
                response.load_rank_match = new Response.LoadRankMatch
                {
                    RankBaseLosePoint = 1,
                    RankBaseWinPoint = 1,
                    RankLoseResultBonus = 1,
                    RankMatchNumDays = 0,
                    RankWinPointBonus1 = 1,
                    RankWinPointBonus2 = 1,
                    RankWinPointBonus3 = 1,
                    RankLosePointBonus1 = 1,
                    RankLosePointBonus2 = 1,
                    RankLosePointBonus3 = 1,
                    ExxThresholdOrder = 1,
                    ExxThresholdPoint = 1
                };
                break;
            case MethodType.MthdSaveLog:
                response.save_log = new Response.SaveLog
                {
                    LoadGameDataVer = 1
                };
                break;
            case MethodType.MthdCheckCommunication:
                response.check_communication = new Response.CheckCommunication();
                break;
            case MethodType.MthdLoadBlackList:
                response.load_black_list = new Response.LoadBlackList
                {
                    ThresholdDelayedRtt = 300,
                    DelayPermitCondition = 300,
                    DelayRestrictCondition = 300,
                    MaxBlacklistNum = 1,
                    ThresholdDelayedFrame = 5
                };
                break;
            case MethodType.MthdSaveTournamentResult:
                response.save_tournament_result = new Response.SaveTournamentResult();
                break;
            case MethodType.MthdLoadRanking:
                response.load_ranking = new Response.LoadRanking();
                break;
            case MethodType.MthdCheckTelop:
                response.check_telop = new Response.CheckTelop
                {
                    Telop1Id = 1
                };
                break;
            case MethodType.MthdLoadTelop:
                response.load_telop = new Response.LoadTelop
                {
                    TelopData = "Test telop"
                };
                break;
            case MethodType.MthdCheckMovieRelease:
                response.check_movie_release = new Response.CheckMovieRelease();
                break;
            case MethodType.MthdLoadSpotUrl:
                response.load_spot_url = new Response.LoadSpotUrl
                {
                    Url = "https://example.com", 
                    Qrcode = "https://example.com"
                };
                break;
            case MethodType.MthdSaveBattleLog:
                response.save_battle_log = new Response.SaveBattleLog();
                break;
            case MethodType.MthdSaveBattleLogOn:
                response.save_battle_log_on = new Response.SaveBattleLogOn();
                break;
            case MethodType.MthdSaveVsmResult:
                response.save_vsm_result = new Response.SaveVsmResult();
                break;
            case MethodType.MthdSaveVsmOnResult:
                response.save_vsm_on_result = new Response.SaveVsmOnResult();
                break;
            case MethodType.MthdSaveVscResult:
                response.save_vsc_result = new Response.SaveVscResult();
                break;
            case MethodType.MthdSaveVstResult:
                response.save_vst_result = new Response.SaveVstResult();
                break;
            case MethodType.MthdSaveVsmToResult:
                response.save_vsm_to_result = new Response.SaveVsmToResult();
                break;
            case MethodType.MthdSaveCharge:
                response.save_charge = new Response.SaveCharge();
                foreach (var unused in request.save_charge.ChargeDatas)
                {
                    response.save_charge.SaveChargeResults.Add(new Response.SaveCharge.SaveChargeResult
                    {
                        hc2_error = nue.protocol.exvs.Response.SaveCharge.SaveChargeResult.Hc2Error.Hc2Success
                    });
                }
                break;
            case MethodType.MthdSaveUserPlayResearchData:
                response.save_user_play_research_data = new Response.SaveUserPlayResearchData();
                break;
            case MethodType.MthdRegisterCard:
            case MethodType.MthdSaveMedal:
            case MethodType.MthdSaveResultCapture:
            case MethodType.MthdSaveFailedBattleLogOn:
            case MethodType.MthdLoadAccessCode:
            case MethodType.MthdUsePCoinTicket:
            case MethodType.MthdLoadSpotInfo:
            case MethodType.MthdLoadReplayCard:
            case MethodType.MthdPreSaveReplay:
            case MethodType.MthdLoadMeetingCard:
            case MethodType.MthdStartTournament:
            case MethodType.MthdPreLoadCard:
            case MethodType.MthdLoadCard:
            default:
                return NotFound();
        }

        return Ok(response);
    }

    [Route("")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello");
    }
}
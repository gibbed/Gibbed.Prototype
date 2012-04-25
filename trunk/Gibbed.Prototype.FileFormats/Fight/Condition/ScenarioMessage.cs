/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    public enum ScenarioMessageType : ulong
    {
        // ReSharper disable InconsistentNaming
        GameInit = 0xC1F17BC6C1174AA4, // "Game Init" : 1
        ObjectDestroyed = 0x183A846171487A68, // "Object Destroyed" : 5
        ObjectStateChanged = 0x104017A46612FA06, // "Object State Changed" : 6
        String = 0xFDFDE79A0EEB6617, // "String" : 2
        TriggerVolumeEnter = 0xCFE11661407B9DC9, // "Trigger Volume - Enter" : 3
        TriggerVolumeExit = 0x1A485821709C2465, // "Trigger Volume - Exit" : 4
        NISBegin = 0x9EE72EC7016317E9, // "NIS Begin" : 11
        NISEnd = 0x692DC41D685C84F9, // "NIS End" : 12
        NISLoadComplete = 0x765D66912662D5DF, // "NIS Load Complete" : 13
        StreamPackageLoaded = 0xED342ECBFFC87543, // "Stream Package Loaded" : 16
        StreamPackageUnloaded = 0x5D9CFEEE31F4373E, // "Stream Package Unloaded" : 17
        DestinationReached = 0x0064642D16D6126A, // "Destination Reached" : 26
        DisguiseChanged = 0x87907AAA7E82DAAD, // "Disguise Changed" : 21
        FixedTargetDead = 0xD80D0E21CD5F5781, // "Fixed Target Dead" : 28
        ObjectSpawned = 0x885B864FAB4D03B3, // "Object Spawned" : 20
        HeliLandedStateChange = 0xE0468D38D899A1A9, // "Heli Landed State Change" : 30
        ObjectConsumed = 0x4D995F4376C73E05, // "Object Consumed" : 19
        ObjectDamaged = 0xAAC76588F2C474E8, // "Object Damaged" : 22
        ObjectDead = 0xBF41A34600300D35, // "Object Dead" : 23
        ObjectZeroHealth = 0x47D1EAFF99622C95, // "Object Zero Health" : 24
        ObjectOffscreenDespawn = 0x7D6E4A2AC37C2058, // "Object Offscreen Despawn" : 25
        TargetChanged = 0x1ED7ED8BE34B0123, // "Target Changed" : 27
        HintCancelled = 0x5094F6F1FB33BE6A, // "Hint Cancelled" : 27
        StateMachineStateChange = 0xEF9D2B7906FC0379, // "State Machine State Change" : 48
        EnterAlert = 0xEBD64263A3F643EC, // "Enter Alert" : 31
        LeaveAlert = 0x36152A4C830744FF, // "Leave Alert" : 32
        DisguiseBlown = 0x3640C32F8E1832B3, // "Disguise Blown" : 34
        DisguiseUnblown = 0x17558F4B11AD09A8, // "Disguise Unblown" : 35
        PlayerWatched = 0x30ED8ABD1A6D02C1, // "Player Watched" : 36
        PlayerUnwatched = 0x04355B6743FF10F2, // "Player Unwatched" : 37
        BaseEnterAlert = 0x25D8568C1BAB6A5F, // "Base Enter Alert" : 38
        BaseLeaveAlert = 0x9958B5A9237F9C44, // "Base Leave Alert" : 39
        BaseBackToIdle = 0x0C023E4D9906A66F, // "Base Back To Idle" : 40
        BaseEnterBeginCombat = 0x33AE18A075740756, // "Base Enter BeginCombat" : 41
        DisguiseHUDWhite = 0xE96052D527272BA9, // "Disguise HUD White" : 42
        DisguiseHUDYellow = 0x488093605C86B9B2, // "Disguise HUD Yellow" : 43
        DisguiseHUDRed = 0xEEE3C87E850DDD83, // "Disguise HUD Red" : 44
        DisguiseHUDRedHide = 0x54BE09E3DFC76F05, // "Disguise HUD Red Hide" : 45
        PlayerEngaged = 0x0C48CB62C87A5188, // "Player Engaged" : 46
        RadioAttempt = 0x9BE1934CDCF9A96A, // "Radio Attempt" : 47
        SpawnerAllSpawned = 0x1E80B98E916FAED3, // "Spawner All Spawned" : 52
        SpawnerFinished = 0x85C37E88199FB8AE, // "Spawner Finished" : 53
        SpawnerAllZeroHealth = 0x2F1BC959A15E2875, // "Spawner All Zero Health" : 54
        SquadSpawned = 0x32F2270A31B95334, // "Squad Spawned" : 55
        SquadDisbanded = 0x8CAD71326A7F78A0, // "Squad Disbanded" : 56
        EnterVehicle = 0xDB940E8E6D8F047E, // "Enter Vehicle" : 57
        ExitVehicle = 0x0CB2ED4EDC863CA0, // "Exit Vehicle" : 58
        ObjectGrabbed = 0xA7C39E6263C14D7E, // "Object Grabbed" : 59
        ObjectDropped = 0xDA8584818ECD4A73, // "Object Dropped" : 60
        ObjectThrown = 0x548C987E799B0129, // "Object Thrown" : 61
        MotionStateChanged = 0x9C66EEAD6AD3F619, // "Motion State Changed" : 62
        SupportingSurfaceChanged = 0xEA3C5073ADA2938E, // "Supporting Surface Changed" : 63
        CollectibleSpawned = 0x488FDDF7DF67BA6C, // "Collectible Spawned" : 64
        CollectibleCollected = 0x469B95A411AAF027, // "Collectible Collected" : 65
        CollectibleEndOfLife = 0x299821107F39D0F2, // "Collectible End Of Life" : 66
        WOINodeStateChanged = 0x0EEB001F9911806E, // "WOI Node State Changed" : 67
        WOIFMVPlayed = 0x5EC8B7C62AC3AC5D, // "WOI FMV Played" : 68
        PatrolDestroyed = 0xEFFCA40E6212A087, // "Patrol Destroyed" : 69
        StrikeTeamDestroyed = 0x74E725E023476D54, // "Strike Team Destroyed" : 70
        StrikeTeamEvaded = 0x6E8404849630B186, // "Strike Team Evaded" : 71
        StrikeTeamInbound = 0x5FA0D5DEF2EBC93A, // "Strike Team Inbound" : 72
        BaseOrHiveDestroyed = 0xD1C7E6D2AA83254D, // "Base or Hive Destroyed" : 73
        BaseOrHiveGeneric = 0xD9A18B6CD97DC1DB, // "Base or Hive Generic" : 74
        AboutToSaveTheGame = 0xF4EC8F708B348712, // "About to Save the Game" : 75
        AboutToSaveACheckpoint = 0xB1C2665463E57916, // "About to Save a Checkpoint" : 76
        JustLoadedACheckpoint = 0x8E8BE0D005C46276, // "Just loaded a Checkpoint" : 77
        AboutToLoadACheckpoint = 0x060EA1B4117386CB, // "About to Load a Checkpoint" : 78
        FreeRoamEvent = 0x8535ADE5A33F15C1, // "Free Roam Event" : 79
        FramerCameraEvent = 0x1D6239543D567B1C, // "Framer Camera Event" : 80
        NISDone = 0xA65FAB59423A58EA, // "NIS Done" : 81
        WOITargetSpawned = 0x5AABCAE5C1B8AA90, // "WOI Target Spawned" : 82
        WOITargetConsumed = 0x0FAD0CC09F284F04, // "WOI Target Consumed" : 83
        UnlockableAcquired = 0xCAB610AF5094EC20, // "Unlockable Acquired" : 84
        UnlockableIncrementedPurchaseType = 0x429FC1442285104F, // "Unlockable Incremented Purchase Type" : 85
        // ReSharper restore InconsistentNaming
    }

    [KnownCondition(typeof(Context.Scenario), "event")]
    public class ScenarioMessage : ConditionBase
    {
        public ScenarioMessageType Type;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.Type = fight.ReadPropertyEnum<ScenarioMessageType>(input);
        }
    }
}

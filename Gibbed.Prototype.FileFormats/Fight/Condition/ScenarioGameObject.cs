using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    public enum ScenarioGameObjectSlot : ulong
    {
        CollectibleCollectedCollectible = 0x01AC19B7AB696308, // "Collectible Collected - Collectible"
        SpawnerFinishedMessageSpawner = 0x027800DF90A433D2, // "Spawner Finished Message - Spawner"
        EnterAlertPlayer = 0x06DA724E63BD8FE8, // "Enter Alert - player"
        CollectibleEndOfLifeGameObject = 0x085C67BBAC8F31DC, // "Collectible End Of Life - Game Object"
        ObjectZeroHealthOriginator = 0x0CCE3134F382733E, // "Object Zero Health - Originator"
        StrikeTeamInboundTarget = 0x17B51EE883EBB05E, // "Strike Team Inbound - Target"
        StrikeTeamEvadedOriginator = 0x1FF9505E86D408E7, // "Strike Team Evaded - Originator"
        TriggerVolumeEnterTouch = 0x2214319353589394, // "Trigger Volume Enter - Touch"
        SupportingSurfaceChangedPlayer = 0x255CBD05E2AF5192, // "Supporting Surface Changed - Player"
        ExitVehicleMessageVehicle = 0x284B8FD3A88226BC, // "Exit Vehicle Message - Vehicle"
        SquadDisbandedMessageSquad = 0x2C72E12FA85B063A, // "Squad Disbanded Message - Squad"
        SpawnerAllSpawnedMessageSpawner = 0x2CE484017731FD1F, // "Spawner All Spawned Message - Spawner"
        ObjectConsumedObject = 0x2EE516D195C5CD65, // "Object Consumed - Object"
        StringMessageGameObject4 = 0x31F350C5BE44408A, // "String message - GameObject 4"
        StringMessageGameObject2 = 0x31F350C5BE44408C, // "String message - GameObject 2"
        StringMessageGameObject3 = 0x31F350C5BE44408D, // "String message - GameObject 3"
        StringMessageGameObject1 = 0x31F350C5BE44408F, // "String message - GameObject 1"
        ObjectSpawnedParent = 0x356DC83AE07AFF5A, // "Object Spawned - Parent"
        ObjectDamagedObject = 0x38ADAB391B5AD94A, // "Object Damaged - Object"
        ObjectGrabbedMessageGrabbed = 0x3C80D4D3165B42E5, // "Object Grabbed Message - Grabbed"
        ObjectGrabbedMessageGrabber = 0x3C80D4D3165B42F3, // "Object Grabbed Message - Grabber"
        FixedTargetDeadObject = 0x436D90A3D76612F9, // "Fixed Target Dead - Object"
        DestinationReachedObject = 0x444C382402166340, // "Destination Reached - Object"
        CollectibleSpawnedCollectible = 0x4AE4CF3ADD41D453, // "Collectible Spawned - Collectible"
        RadioAttemptRadioer = 0x4E45360B404E2963, // "Radio Attempt - radioer"
        ObjectConsumedOriginator = 0x50D68ABF854E3DAE, // "Object Consumed - Originator"
        ObjectOffscreenDespawnObject = 0x54A1B29C571139FA, // "Object Offscreen Despawn - Object"
        StrikeTeamInboundStrikeTeamObject = 0x55A892B776FA059F, // "Strike Team Inbound - Strike Team Object"
        ObjectThrownMessageObject = 0x59C1D9B169CB4026, // "Object Thrown Message - Object"
        SpawnerAllZeroHealthMessageSpawner = 0x5C75052F86FDA7CD, // "Spawner All Zero Health Message - Spawner"
        ObjectZeroHealthObject = 0x5C9FCAF5370FA2F5, // "Object Zero Health - Object"
        ObjectThrownMessageFrom = 0x5CB199F2C98DE19B, // "Object Thrown Message - From"
        StrikeTeamDestroyedOriginator = 0x5D98AD6108EB9DB9, // "Strike Team Destroyed - Originator"
        StreamPackageUnloaded = 0x5D9CFEEE31F4373E, // "Stream Package Unloaded"
        NISLoadComplete = 0x626373FF63C9E07B, // "NIS LoadComplete"
        NISEnd = 0x692DC41D685C84F9, // "NIS End"
        ObjectDamagedOriginator = 0x6C327431D15AE905, // "Object Damaged - Originator"
        WebOfIntrigueNodeFMVPlayedMessage = 0x707306A8F6F0A6B7, // "Web Of Intrigue Node FMV Played Message"
        TriggerVolumeExitTouch = 0x7AAE8E41A0C22042, // "Trigger Volume Exit - Touch"
        FixedTargetDeadTarget = 0x7AF6EAB8439C352B, // "Fixed Target Dead - Target"
        StrikeTeamEvadedStrikeTeamObject = 0x7E10840B6F27785B, // "Strike Team Evaded - Strike Team Object"
        ObjectSpawnedObject = 0x864F000232B2C257, // "Object Spawned - Object"
        TargetChangedObject = 0x86CA2E3007E300C7, // "Target Changed - Object"
        LeaveAlertPlayer = 0x8D542881349980B1, // "Leave Alert - player"
        StateMachineStateChangeStateMachine = 0x96C01146F49298A6, // "State Machine State Change - StateMachine"
        ObjectThrownMessageTo = 0x98B44D00F2CA3154, // "Object Thrown Message - To"
        EnterVehicleMessageCharacter = 0x9E78C2F5C1F8F02B, // "Enter Vehicle Message - Character"
        NISBegin = 0x9EE72EC7016317E9, // "NIS Begin"
        SquadSpawnedMessageSquad = 0xA08A944CBC3B5956, // "Squad Spawned Message - Squad"
        ObjectDeadOriginator = 0xAA2A6F28A7C22A9E, // "Object Dead - Originator"
        ObjectDestroyedObject = 0xAF91A187A30333CA, // "Object Destroyed - Object"
        DisguiseBlownPlayer = 0xB0B45A26C0879FF5, // "Disguise Blown - player"
        PlayerEngagedPlayer = 0xB3BF4EFC55AE0C04, // "Player Engaged - player"
        DisguiseUnblownPlayer = 0xB513122C20B15C24, // "Disguise Unblown - player"
        ObjectDroppedMessageGrabber = 0xC1D1A14F18C5916E, // "Object Dropped Message - Grabber"
        MotionStateMessagePlayer = 0xC55B1045A7B08EA8, // "Motion State Message - Player"
        ExitVehicleMessageCharacter = 0xC5E55B8F5823B699, // "Exit Vehicle Message - Character"
        StrikeTeamDestroyedStrikeTeamObject = 0xC65673CB26BA76C9, // "Strike Team Destroyed - Strike Team Object"
        SquadSpawnedMessageSpawner = 0xCA02C894BF75964C, // "Squad Spawned Message - Spawner"
        PlayerWatchedPlayer = 0xCBF621477219290B, // "Player Watched - player"
        ObjectDeadObject = 0xD2BFCE1EF6BADA55, // "Object Dead - Object"
        ObjectStateChangedObject = 0xD529C4C7F4630A94, // "Object State Changed - Object"
        DisguiseChangedObject = 0xD978C27465C1078D, // "Disguise Changed - Object"
        PlayerUnwatchedPlayer = 0xD9D1069641ED1276, // "Player Unwatched - player"
        TriggerVolumeExitTrigger = 0xDDA5CA9C7F972153, // "Trigger Volume Exit - Trigger"
        ObjectDroppedMessageDropped = 0xE238078313D9556D, // "Object Dropped Message - Dropped"
        TargetChangedPreviousTarget = 0xE3C56334ED7338F8, // "Target Changed - Previous Target"
        TriggerVolumeEnterTrigger = 0xE5CB9C69AC3A588D, // "Trigger Volume Enter - Trigger"
        StreamPackageLoaded = 0xED342ECBFFC87543, // "Stream Package Loaded"
        EnterVehicleMessageVehicle = 0xF120C8BE022DCB16, // "Enter Vehicle Message - Vehicle"
    }

    [KnownCondition(typeof(Context.Scenario), "gameObject")]
    public class ScenarioGameObject : ConditionBase
    {
        public ScenarioGameObjectSlot GameObjectSlot;
        public UInt64 GameObjectNameHash;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            UInt64 type = input.ReadU64();
            if (Enum.IsDefined(typeof(ScenarioGameObjectSlot), type) == false)
            {
                throw new Exception(FightHashes.Lookup(type));
            }
            this.GameObjectSlot = (ScenarioGameObjectSlot)(type);

            this.GameObjectNameHash = fight.ReadNameHash(input);
        }
    }
}

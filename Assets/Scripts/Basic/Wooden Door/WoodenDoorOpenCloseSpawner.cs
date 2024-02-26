using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Burst;


[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct WoodenDoorOpenCloseSpawner : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<WoodenDoorData>();
    }

    [BurstCompile]
    public partial struct DoorTriggerEvents : ITriggerEventsJob
    {
        public EntityManager        entityManager;
        public NativeArray<int>     openState;
        public NativeArray<Entity>  triggered;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity door         = (entityManager.HasComponent<WoodenDoorData>(triggerEvent.EntityA))      ? triggerEvent.EntityA : triggerEvent.EntityB;
            Entity player       = (entityManager.HasComponent<PlayerTagComponent>(triggerEvent.EntityB))    ? triggerEvent.EntityB : triggerEvent.EntityA;

            openState[0]        = (entityManager.HasComponent<PlayerTagComponent>(player)) ? 1 : 0;
            triggered[0]        = door;
            
        }
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer     ecb                 = new EntityCommandBuffer(Allocator.TempJob);
        NativeArray<int>        openState           = new NativeArray<int>(1, Allocator.TempJob);
        NativeArray<Entity>     triggered           = new NativeArray<Entity>(1, Allocator.TempJob);
        DoorTriggerEvents       doorTriggerEvents   = new DoorTriggerEvents()
        {
            entityManager   = state.EntityManager,
            openState       = openState,
            triggered       = triggered
        };

        doorTriggerEvents.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

        // If the player is over a door.
        if (openState[0] == 1)
        {
            CheckPlayerCollisionJob     checkPlayerCollisionJob         = new CheckPlayerCollisionJob()  { triggeredEntities = triggered };
            OpenDoorEntitySpawnJob      openDoorEntitySpawnJob          = new OpenDoorEntitySpawnJob()   { ecb = ecb };
            
            checkPlayerCollisionJob.Schedule(state.Dependency).Complete();      // If the door is colliding with the player, then set 'isOpen' to 'true'
            openDoorEntitySpawnJob.Schedule(state.Dependency).Complete();       // If the door is open then spawn a single new open door enity
        }
        // If the player is NOT over a door.
        else
        {
            DestroyOpenDoorEntityJob    destroyOpenDoorEntityJob        = new DestroyOpenDoorEntityJob() { ecb = ecb };
            ResetWoodenDoorsJob         resetDoorsJob                   = new ResetWoodenDoorsJob()      { ecb = ecb };

            destroyOpenDoorEntityJob.Schedule(state.Dependency).Complete();     // Destroy any open door entities that were spawned
            resetDoorsJob.Schedule(state.Dependency).Complete();                // Reset the values of any doors the player triggered
        }

        ecb.Playback(state.EntityManager);
        openState.Dispose();
        triggered.Dispose();
    }


}

#region IJobEntity Section
[BurstCompile]
public partial struct DestroyOpenDoorEntityJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(OpenWoodenDoorTagComponent openDoor, Entity entity) => ecb.DestroyEntity(entity);
}
[BurstCompile]
public partial struct ResetWoodenDoorsJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(WoodenDoorAspect doorAspect) => doorAspect.ResetDoorData(ecb);
}
[BurstCompile]
public partial struct CheckPlayerCollisionJob : IJobEntity
{
    public NativeArray<Entity> triggeredEntities;
    public void Execute(WoodenDoorAspect doorAspect, Entity entity) => doorAspect.SetIsOpenTo((entity == triggeredEntities[0]));
}
[BurstCompile]
public partial struct OpenDoorEntitySpawnJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(WoodenDoorAspect doorAspect) => doorAspect.SpawnAndSetNewOpenDoor(ecb);
}

#endregion
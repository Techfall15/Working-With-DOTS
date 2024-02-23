using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Burst;
using JetBrains.Annotations;

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
        public EntityManager entityManager;
        public NativeArray<int> openState;
        public NativeArray<Entity> triggered;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity door         = triggerEvent.EntityB;
            Entity player       = triggerEvent.EntityA;

            openState[0] = (entityManager.HasComponent<PlayerTagComponent>(player)) ? 1 : 0;
            triggered[0] = door;
            
        }
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        NativeArray<int> openState = new NativeArray<int>(1, Allocator.TempJob);
        NativeArray<Entity> triggered = new NativeArray<Entity>(1, Allocator.TempJob);
        DoorTriggerEvents doorTriggerEvents = new DoorTriggerEvents()
        {
            entityManager = state.EntityManager,
            openState = openState,
            triggered = triggered
        };

        doorTriggerEvents.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

        
        
        // If the player is over a door.
        if (openState[0] == 1)
        {
            // For each door entity
            foreach(var(woodenDoorData, doorLocalTransform, entity) in SystemAPI.Query<RefRW<WoodenDoorData>, RefRO<LocalTransform>>().WithEntityAccess())
            {
                // If the door is colliding with the player, then set 'isOpen' to 'true'
                woodenDoorData.ValueRW.isOpen = (entity == triggered[0]) ? true : false;

                // If the door is colliding with the player AND hasn't already spawned the open door entity
                if(woodenDoorData.ValueRO.isOpen == true && woodenDoorData.ValueRO.hasOpenDoorBeenSpawned == false)
                {
                    // Spawn open door entity and set transform next to the door
                    Entity newOpenDoorEntity = ecb.Instantiate(woodenDoorData.ValueRO.openDoorEntity);
                    float3 doorPos = doorLocalTransform.ValueRO.Position;

                    LocalTransform openDoorTransform = new LocalTransform()
                    {
                        Position = new float3(doorPos.x - 0.9f, doorPos.y, doorPos.z),
                        Rotation = Quaternion.identity,
                        Scale = 1f
                    };
                    // Change 'hasOpenDoorBeenSpawned' to true, so only 1 gets spawned
                    woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = true;
                    ecb.Instantiate(woodenDoorData.ValueRO.openDoorEntityAudioSource);
                    // Set the spawn position of the open door entity
                    ecb.SetComponent<LocalTransform>(newOpenDoorEntity, openDoorTransform);
                }
            }
        }
        // If the player is not over a door.
        else
        {
            // For each open door entity that was spawned
            foreach(var(openWoodenDoorTag, entity) in SystemAPI.Query<RefRO<OpenWoodenDoorTagComponent>>().WithEntityAccess())
            {
                // Destroy it
                ecb.DestroyEntity(entity);
            }
            // For each door entity
            foreach (var woodenDoorData in SystemAPI.Query<RefRW<WoodenDoorData>>())
            {
                // Check if the door was closed already, if so then continue to next loop
                if (woodenDoorData.ValueRO.isOpen == false) continue;
                // If the door was open, then close it and reset values
                woodenDoorData.ValueRW.isOpen = false;
                woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = false;
                // Finally, play the closing sound
                ecb.Instantiate(woodenDoorData.ValueRO.closeDoorEntityAudioSource);
            }
        }

        ecb.Playback(state.EntityManager);
        openState.Dispose();
        triggered.Dispose();
    }


}

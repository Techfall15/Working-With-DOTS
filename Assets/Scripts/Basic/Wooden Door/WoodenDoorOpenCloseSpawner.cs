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

        
        

        if (openState[0] == 1)
        {
            // For each door entity
            foreach(var(woodenDoorData, doorLocalTransform, entity) in SystemAPI.Query<RefRW<WoodenDoorData>, RefRO<LocalTransform>>().WithEntityAccess())
            {
                woodenDoorData.ValueRW.isOpen = (entity == triggered[0]) ? true : false;

                if(woodenDoorData.ValueRO.isOpen == true && woodenDoorData.ValueRO.hasOpenDoorBeenSpawned == false)
                {
                    Entity newOpenDoorEntity = ecb.Instantiate(woodenDoorData.ValueRO.openDoorEntity);
                    float3 doorPos = doorLocalTransform.ValueRO.Position;

                    LocalTransform openDoorTransform = new LocalTransform()
                    {
                        Position = new float3(doorPos.x - 0.9f, doorPos.y, doorPos.z),
                        Rotation = Quaternion.identity,
                        Scale = 1f
                    };
                    woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = true;
                    ecb.SetComponent<LocalTransform>(newOpenDoorEntity, openDoorTransform);
                }
            }
        }
        else
        {
            foreach (var woodenDoorData in SystemAPI.Query<RefRW<WoodenDoorData>>())
            {
                if (woodenDoorData.ValueRO.isOpen == false) continue;
                woodenDoorData.ValueRW.isOpen = false;
                woodenDoorData.ValueRW.hasOpenDoorBeenSpawned = false;
            }
            // For each open door entity
            foreach(var(openWoodenDoorTag, entity) in SystemAPI.Query<RefRO<OpenWoodenDoorTagComponent>>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
        }

        ecb.Playback(state.EntityManager);
        openState.Dispose();
        triggered.Dispose();
    }


}

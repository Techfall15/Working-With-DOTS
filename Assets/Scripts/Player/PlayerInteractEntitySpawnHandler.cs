using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct PlayerInteractEntitySpawnHandler : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerInteractData>();
    }


    [BurstCompile]
    public partial struct SpawnInteractEntityEvent : ITriggerEventsJob
    {
        public EntityManager entityManager;
        public NativeArray<int> triggerEntitySpawn;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity interactEntity = triggerEvent.EntityB;
            
            // Only allow spawing if distance is half of the player size, or 0.5f.
            triggerEntitySpawn[0] = (entityManager.HasComponent<InteractableTagComponent>(interactEntity) == true) ? 1 : 0;
        }
    }


    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb                         = new EntityCommandBuffer(Allocator.Temp);
        NativeArray<int> triggerEntitySpawn             = new NativeArray<int>(1, Allocator.TempJob);
        SpawnInteractEntityEvent interactEntityEvent    = new SpawnInteractEntityEvent()
        {
            entityManager       = state.EntityManager,
            triggerEntitySpawn  = triggerEntitySpawn,
        };

        interactEntityEvent.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

        if (triggerEntitySpawn[0] == 1)
        {
            foreach (var (playerInteractData, playerLocalTransform) in SystemAPI.Query<RefRW<PlayerInteractData>, RefRO<LocalTransform>>())
            {
                if (playerInteractData.ValueRO.isInteractEntitySpawned == false)
                {
                    Entity interactEntity   = ecb.Instantiate(playerInteractData.ValueRO.interactEntity);
                    float3 playerPos        = playerLocalTransform.ValueRO.Position;
                    LocalTransform interactTransform = new LocalTransform()
                    {
                        Position    = new float3(playerPos.x, playerPos.y, playerPos.z),
                        Rotation    = Quaternion.identity,
                        Scale       = 1f,
                    };
                    ecb.SetComponent<LocalTransform>(interactEntity, interactTransform);
                    playerInteractData.ValueRW.isInteractEntitySpawned = true;
                }
            }
        }
        if (triggerEntitySpawn[0] == 0)
        {
            foreach (var (interactTag, entity) in SystemAPI.Query<RefRO<InteractEntityTagComponent>>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
                foreach(var playerInteractData in SystemAPI.Query<RefRW<PlayerInteractData>>())
                {
                    playerInteractData.ValueRW.isInteractEntitySpawned = false;
                }
            }
        }
        

        ecb.Playback(state.EntityManager);
        triggerEntitySpawn.Dispose();
    }

}
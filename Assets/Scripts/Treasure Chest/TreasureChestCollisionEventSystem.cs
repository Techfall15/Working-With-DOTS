using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct TreasureChestCollisionEventSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TreasureChestData>();
    }


    [BurstCompile]
    public partial struct TreasureChestCollisionEvents : ITriggerEventsJob
    {
        public NativeArray<int>     testCount;
        public EntityManager        entityManager;
        public NativeArray<Entity>  entityArray;
        

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entity = triggerEvent.EntityB;

            if (entityManager.HasComponent<PlayerTagComponent>(triggerEvent.EntityA))
            {
                testCount[0] += 1;
            }
            entityArray[0] = entity;
            
        }
    }
    
    public void OnUpdate(ref SystemState state)
    {
        
        NativeArray<int> testCounts     = new NativeArray<int>(1, Allocator.TempJob);
        NativeArray<Entity> entityArray = new NativeArray<Entity>(1, Allocator.TempJob);

        testCounts[0] = 0;
        var job = new TreasureChestCollisionEvents()
        {
            testCount = testCounts,
            entityArray = entityArray,
            entityManager = state.EntityManager
        };
        job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();

        foreach(var(chestData, entity) in SystemAPI.Query<RefRW<TreasureChestData>>().WithEntityAccess())
        {
            if (entity != entityArray[0])   chestData.ValueRW.canOpen = false;
            else                            chestData.ValueRW.canOpen = true;
        }

        
        testCounts.Dispose();
        entityArray.Dispose();
    }
}
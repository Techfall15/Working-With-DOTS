using Unity.Entities;
using Unity.Physics;
using Unity.Burst;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsInitializeGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct GoldCoinCollisionEventSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GoldCoinData>();
    }

    
    public partial struct GoldCoinCollisionEvents : ITriggerEventsJob
    {
        
        public EntityManager entityManager;
        public NativeArray<Entity> entityArray;
        public void Execute(TriggerEvent triggerEvent)
        {
            if (entityManager.HasComponent<GoldCoinData>(triggerEvent.EntityA) || entityManager.HasComponent<GoldCoinData>(triggerEvent.EntityB) == false) return;
            Entity coinEntity = (entityManager.HasComponent<GoldCoinData>(triggerEvent.EntityA)) ? triggerEvent.EntityA : triggerEvent.EntityB;

            entityArray[0] = coinEntity;
            
        }
    }

    public void OnUpdate(ref SystemState state)
    {
        NativeArray<Entity> coinEntities = new NativeArray<Entity>(1, Allocator.TempJob);
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        var job = new GoldCoinCollisionEvents()
        {
            entityManager = state.EntityManager,
            entityArray = coinEntities
        };
        
        job.Schedule<GoldCoinCollisionEvents>(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency).Complete();


        if (state.EntityManager.HasComponent<GoldCoinData>(coinEntities[0]) == true)
        {
            foreach( var (coinData, entity) in SystemAPI.Query<RefRW<GoldCoinData>>().WithEntityAccess())
            {
                if(entity == coinEntities[0])
                {
                    coinData.ValueRW.isAnimating = false;
                }
            }
            ecb.DestroyEntity(coinEntities);
        }
        ecb.Playback(state.EntityManager);
        coinEntities.Dispose();
        ecb.Dispose();
    }
}
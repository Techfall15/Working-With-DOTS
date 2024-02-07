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
public partial struct MedalCollisionEventsSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MedalData>();
    }



    [BurstCompile]
    public partial struct MedalCollisionEvents : ITriggerEventsJob
    {
        public NativeArray<int> scoreValues;
        public EntityManager entityManager;
        public NativeArray<Entity> medalEntity;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entity       = (entityManager.HasComponent<MedalData>(triggerEvent.EntityA)) ? triggerEvent.EntityA : triggerEvent.EntityB;
            var scoreValue      = entityManager.GetComponentData<MedalData>(entity).scoreValue;

            scoreValues[0]      = scoreValue;
            medalEntity[0]      = entity;

        }
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        
        NativeReference<int>    numCollisionEvents  = new NativeReference<int>(0, Allocator.TempJob);
        NativeArray<Entity>     medalEntity         = new NativeArray<Entity>(1, Allocator.TempJob);
        NativeArray<int>        scoreValues         = new NativeArray<int>(1, Allocator.TempJob);

        var job = new MedalCollisionEvents
        {
            entityManager   = state.EntityManager,
            medalEntity     = medalEntity,
            scoreValues     = scoreValues,
        };

        job.Schedule<MedalCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
            state.Dependency
            ).Complete();

        NativeArray<Entity> medalEntities = new NativeArray<Entity>(10, Allocator.Temp);
        if (scoreValues[0] > 0) 
        {
            int childCount = 1;
            medalEntities[0] = medalEntity[0];
            if (state.EntityManager.HasBuffer<Child>(medalEntities[0]))
            {
                foreach(var child in state.EntityManager.GetBuffer<Child>(medalEntity[0]))
                {
                    medalEntities[childCount] = child.Value;
                    childCount++;
                }
                for(int i = 1; i < medalEntities.Length; i++)
                {
                    if (state.EntityManager.HasBuffer<Child>(medalEntities[i]))
                    {
                        foreach (var child in state.EntityManager.GetBuffer<Child>(medalEntities[i]))
                        {
                            medalEntities[childCount] = child.Value;
                            childCount++;
                        }
                    }
                }
            }
            ecb.DestroyEntity(medalEntities);
        }

        ecb.Playback(state.EntityManager);
        
        numCollisionEvents.Dispose();
        scoreValues.Dispose();
        medalEntity.Dispose();
        medalEntities.Dispose();
    }
}

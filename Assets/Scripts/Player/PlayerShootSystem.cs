using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public partial struct PlayerShootSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerShootComponent>();
    }
    public void OnUpdate(ref SystemState state)
    {
        
        NativeArray<Entity> newMedals = new NativeArray<Entity>(1, Allocator.TempJob);
        SpawnNewMedalJob spawnNewMedalJob = new SpawnNewMedalJob
        {
            entityManager = state.EntityManager,
            newMedals = newMedals
        };

        foreach(var(localTransform, playerShootComponent, inputData) 
            in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerShootComponent>, RefRW<InputData>>())
        {
            if (inputData.ValueRO.shoot == false) continue;
            LocalTransform newSpawnTransform = new LocalTransform
            {
                Position = localTransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 0.2f
            };
            Entity newObj = state.EntityManager.Instantiate(playerShootComponent.ValueRO.objToSpawnEntity);
            state.EntityManager.SetComponentData<LocalTransform>(newObj, newSpawnTransform);
        }
        spawnNewMedalJob.Schedule(state.Dependency).Complete();
        if (state.EntityManager.HasComponent<MedalData>(newMedals[0]))
        {
            state.EntityManager.Instantiate(newMedals[0]);
            foreach((RefRW<LocalTransform> localTransform, RefRO<MedalData> medalData) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MedalData>>())
            {
                localTransform.ValueRW = new LocalTransform
                {
                    Position = new float3(-3, -3, -3),
                    Rotation = quaternion.identity,
                    Scale = 1f,
                };
            }
        }
        newMedals.Dispose();
    }

}
public partial struct SpawnNewMedalJob : IJobEntity
{
    public NativeArray<Entity> newMedals;
    public EntityManager entityManager;
    public void Execute(in PlayerSpawnComponent playerSpawnComponent, ref InputData inputData)
    {
        if (inputData.spawnMedal == false) return;
        Entity newMedal = playerSpawnComponent.medalToSpawnEntity;
        newMedals[0] = newMedal;
    }
}
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.Animations;

public partial struct PlayerShootSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerShootComponent>();
    }
    public void OnUpdate(ref SystemState state)
    {
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
        foreach(var(localTransform, playerSpawnComponent, inputData)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerSpawnComponent>, RefRW<InputData>>())
        {
            if (inputData.ValueRO.spawnMedal == false) continue;
            LocalTransform newMedalTransform = new LocalTransform
            {
                Position = new float3(-3f, -3f, 0f),
                Rotation = quaternion.identity,
                Scale = 1f
            };
            Entity newMedal = state.EntityManager.Instantiate(playerSpawnComponent.ValueRO.medalToSpawnEntity);
            state.EntityManager.SetComponentData<LocalTransform>(newMedal, newMedalTransform);
        }
    }

}
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
    }

}
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct SpawnNewEntitySystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnNewEntityData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        // Create foreach loop to get reference to spawner entity.
        foreach((RefRW<SpawnNewEntityData> spawnData, RefRO<InputData> inputData, Entity entity) 
            in SystemAPI.Query<RefRW<SpawnNewEntityData>, RefRO<InputData>>().WithEntityAccess())
        {
            bool isSpawnTriggered           = inputData.ValueRO.shoot;
            

            spawnData.ValueRW.spawnPosition = state.EntityManager.GetComponentData<LocalTransform>(entity).Position;
            float3 spawnPos                 = spawnData.ValueRO.spawnPosition;

            if(isSpawnTriggered)
            {
                Entity newEntity            = state.EntityManager.Instantiate(spawnData.ValueRO.newEntityToSpawn);
                LocalTransform newTransform = new LocalTransform()
                {
                    Position                = spawnPos,
                    Rotation                = Quaternion.identity,
                    Scale                   = 1.0f
                };
                state.EntityManager.SetComponentData<LocalTransform>(newEntity, newTransform);
            }
        }
        // Create IJobEntity for faster code.

    }

}
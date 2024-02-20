using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public partial struct TreasureChestParticleHandler : ISystem
{


    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TreasureChestData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach(var (chestData, localTransform) in SystemAPI.Query<RefRW<TreasureChestData>, RefRO<LocalTransform>>())
        {
            if (chestData.ValueRO.canSpawnParticle == true)
            {
                Entity newParticle = state.EntityManager.Instantiate(chestData.ValueRO.particleToSpawn);
                LocalTransform newLocalTransform = new LocalTransform()
                {
                    Position    = localTransform.ValueRO.Position,
                    Rotation    = Quaternion.identity,
                    Scale       = 1f,
                };
                ecb.SetComponent<LocalTransform>(newParticle, newLocalTransform);
                chestData.ValueRW.canSpawnParticle = false;
            }
        }

        ecb.Playback(state.EntityManager);
    }
}
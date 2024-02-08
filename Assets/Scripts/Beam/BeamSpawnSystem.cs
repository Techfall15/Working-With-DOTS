using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using UnityEngine;
public partial struct BeamSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeamSpawnerData>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);


        foreach(RefRW<BeamSpawnerData> beamSpawnerData in SystemAPI.Query<RefRW<BeamSpawnerData>>())
        {
            if(beamSpawnerData.ValueRO.timeUntilSpawn <= 0)
            {
                // Spawn New Beam
                Entity newBeam = ecb.Instantiate(beamSpawnerData.ValueRO.entityToSpawn);
                // Get Spawn Position
                
                LocalTransform newBeamTransform = new LocalTransform
                {
                    Position = beamSpawnerData.ValueRO.spawnPosition,
                    Rotation = Quaternion.Euler(0f,0f,90f),
                    Scale = 1f
                };
                ecb.SetComponent(newBeam, newBeamTransform);
                beamSpawnerData.ValueRW.timeUntilSpawn = beamSpawnerData.ValueRW.timeBetweenSpawns;
            }
            else
            {
                // Reduce time until spawn
                beamSpawnerData.ValueRW.timeUntilSpawn -= SystemAPI.Time.DeltaTime;
            }
        }
        
        ecb.Playback(state.EntityManager);
    }

}

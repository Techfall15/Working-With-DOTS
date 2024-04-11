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
                // Get Random Y Position
                float randomYPos    = UnityEngine.Random.Range(-4.5f, 4.5f);

                // Get Reference To Spawn Position Data
                float3 spawnDataPos = beamSpawnerData.ValueRO.spawnPosition;

                // Set Random Y Position
                beamSpawnerData.ValueRW.spawnPosition = new float3(spawnDataPos.x, randomYPos, 0f);

                // Spawn In New Beam
                Entity newBeam = ecb.Instantiate(beamSpawnerData.ValueRO.entityToSpawn);

                // Create New Spawn Position
                LocalTransform newBeamTransform = new LocalTransform
                {
                    Position = beamSpawnerData.ValueRO.spawnPosition,
                    Rotation = Quaternion.Euler(0f,0f,90f),
                    Scale = 1f
                };

                // Set New Spawn Position
                ecb.SetComponent(newBeam, newBeamTransform);

                // Reset Spawn Timer
                beamSpawnerData.ValueRW.timeUntilSpawn = beamSpawnerData.ValueRW.timeBetweenSpawns;
            }
            else
            {
                // Reduce time until spawn
                beamSpawnerData.ValueRW.timeUntilSpawn -= SystemAPI.Time.DeltaTime;
            }
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

}

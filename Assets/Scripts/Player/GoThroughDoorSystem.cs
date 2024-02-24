using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;


public partial struct GoThroughDoorSystem : ISystem
{


    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InputData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb                 = new EntityCommandBuffer(Allocator.TempJob);
        SpawnQuestionMarkJob questionMarkJob    = new SpawnQuestionMarkJob() { ecb = ecb };


        // For each source of input information
        foreach(var inputData in SystemAPI.Query<RefRO<InputData>>())
        {
            // Check if the player presses the 'goThroughDoor' button
            if (inputData.ValueRO.goThoughDoor == false) continue;

            foreach(var woodenDoorData in SystemAPI.Query<RefRW<WoodenDoorData>>())
            {
                if (woodenDoorData.ValueRO.isOpen == false) continue;
                SetNewPlayerPositionJob newPlayerPosJob = new SetNewPlayerPositionJob()
                {
                    ecb = ecb,
                    newPlayerPos = woodenDoorData.ValueRO.playerSpawnLocation,
                };
                UpdateFadeBoxJob updateFadeBoxJob = new UpdateFadeBoxJob() { newSpawnLocation = woodenDoorData.ValueRO.cameraSpawnLocation };

                newPlayerPosJob.Schedule(state.Dependency).Complete();
                updateFadeBoxJob.Schedule(state.Dependency).Complete();

            }
            questionMarkJob.Schedule(state.Dependency).Complete();
            
        }


        ecb.Playback(state.EntityManager);


    }

}
#region IJobEntity Section
[BurstCompile]
public partial struct SpawnQuestionMarkJob : IJobEntity
{
    public EntityCommandBuffer ecb;

    public void Execute(ref TestNPCData npcData, in LocalTransform npcTransform)
    {

        if(npcData.canSpawnQuestionMark == true && npcData.alreadySpawnedQuestionMark == false)
        {
            Entity questionMark = ecb.Instantiate(npcData.questionMarkEntity);
            LocalTransform questionMarkTransform = new LocalTransform()
            {
                Position = new float3(npcTransform.Position.x, npcTransform.Position.y + 1.25f, npcTransform.Position.z),
                Rotation = Quaternion.identity,
                Scale = 1f
            };
            npcData.alreadySpawnedQuestionMark = true;
            ecb.SetComponent<LocalTransform>(questionMark, questionMarkTransform);
        }
    }
}
[BurstCompile]
public partial struct SetNewPlayerPositionJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public float3 newPlayerPos;
    public void Execute(in PlayerTagComponent playerTag, ref LocalTransform playerTransform, Entity entity)
    {
        float3          newPlayerPosition   = newPlayerPos;
        LocalTransform  newTransform        = new LocalTransform()
        {
            Position    = newPlayerPosition,
            Rotation    = Quaternion.identity,
            Scale       = 1f
        };

        ecb.SetComponent<LocalTransform>(entity, newTransform);
    }
}
[BurstCompile]
public partial struct UpdateFadeBoxJob : IJobEntity
{
    public float3 newSpawnLocation;
    public void Execute(ref FadeBoxData fadeBoxData, in LocalTransform fadeBoxTransform)
    {
        if (fadeBoxData.fadeCount < 2) fadeBoxData.isFading = true;
        fadeBoxData.newPosition = newSpawnLocation;
    }
}



#endregion

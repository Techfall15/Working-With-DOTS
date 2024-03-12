using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;


public partial struct GoThroughDoorSystem : ISystem
{


    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InputData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer     ecb                 = new EntityCommandBuffer(Allocator.TempJob);
        SpawnQuestionMarkJob    questionMarkJob     = new SpawnQuestionMarkJob() { ecb = ecb };
        NativeArray<Entity>     targetDoor          = new NativeArray<Entity>(1, Allocator.TempJob);
        SetTargetDoorJob        setTargetDoorJob    = new SetTargetDoorJob(){ targetDoor = targetDoor };


        // For each source of input information
        foreach(var inputData in SystemAPI.Query<RefRO<InputData>>())
        {
            
            if (inputData.ValueRO.goThoughDoor == false) continue;                   // Check if the player presses the 'goThroughDoor' button

            setTargetDoorJob.Schedule(state.Dependency).Complete();                   // Set targetDoor to the door that the player has opened
            ScheduleMovementJobs(ref state, ecb, targetDoor);


            questionMarkJob.Schedule(state.Dependency).Complete();                    // If able, spawn a question mark over the NPC's head when the player leaves their room       
        }
        ecb.Playback(state.EntityManager);
        targetDoor.Dispose();
        ecb.Dispose();
    }


    public void ScheduleMovementJobs(ref SystemState state, EntityCommandBuffer ecb, NativeArray<Entity> targetDoor)
    {
        if (state.EntityManager.HasComponent<WoodenDoorData>(targetDoor[0]))
        {
            WoodenDoorData doorData = state.EntityManager.GetComponentData<WoodenDoorData>(targetDoor[0]);
            UpdateFadeBoxJob updateFadeBoxJob = new UpdateFadeBoxJob() { newSpawnLocation = doorData.cameraSpawnLocation };
            SetNewPlayerPositionJob newPlayerPosJob = new SetNewPlayerPositionJob()
            {
                ecb = ecb,
                newPlayerPos = doorData.playerSpawnLocation,
            };


            newPlayerPosJob.Schedule(state.Dependency).Complete();                 // Move the player
            updateFadeBoxJob.Schedule(state.Dependency).Complete();                 // Move the fade box
        }
    }

}
#region IJobEntity Section
[BurstCompile]
public partial struct SpawnQuestionMarkJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(TestNPCAspect npcAspect) => npcAspect.SpawnAndSetNewQuestionMark(ecb);
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
    public void Execute(FadeBoxAspect fadeBoxAspect) => fadeBoxAspect.SetNewFadeBoxPosition(newSpawnLocation);
}
[BurstCompile]
public partial struct SetTargetDoorJob : IJobEntity
{
    public NativeArray<Entity> targetDoor;
    public void Execute(ref WoodenDoorData doorData, Entity entity) => targetDoor[0] = (doorData.isOpen == true) ? entity : targetDoor[0];
}

#endregion

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct FadeBoxSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FadeBoxData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        // For each fade box
        foreach ((FadeBoxAspect fadeBoxAspect, Entity entity) in SystemAPI.Query<FadeBoxAspect>().WithEntityAccess())
        {
            float           deltaTime       = SystemAPI.Time.DeltaTime;
            SpriteRenderer  renderer        = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            Color           myCurrentColor  = renderer.color;

            FadeInJob       fadeInJob       = new FadeInJob()
            {
                deltaTime       = deltaTime,
                myCurrentColor  = myCurrentColor,
            };
            FadeOutJob      fadeOutJob      = new FadeOutJob()
            {
                deltaTime       = deltaTime,
                myCurrentColor  = myCurrentColor,
            };

            switch(fadeBoxAspect.fadeBoxData.ValueRO.fadeCount)
            {
                // If the fade box has NOT started fading yet, then fade in
                case 0:
                    fadeInJob.Schedule(state.Dependency).Complete();
                    break;

                // If the fade box has reached max alpha (completely black), move the fade box & camera if necessary, then fade out
                case 1:
                    UpdateFadeBoxPositionJob updateFadeBoxPositionJob = new UpdateFadeBoxPositionJob() { ecb = ecb };
                    updateFadeBoxPositionJob.Schedule(state.Dependency).Complete();
                    Vector3 camPos          = Camera.main.transform.position;
                    float3 newFadeBoxPos    = fadeBoxAspect.fadeBoxData.ValueRO.newPosition;

                    if(camPos.x != newFadeBoxPos.x || camPos.y != newFadeBoxPos.y)
                    {
                        Camera.main.transform.position = new float3(newFadeBoxPos.x, newFadeBoxPos.y, -10f);
                    }

                    fadeOutJob.Schedule(state.Dependency).Complete();
                    break;

                // Reset the fade box back to default values
                case 2:
                    ResetFadeBoxValuesJob resetFadeBoxValueJob = new ResetFadeBoxValuesJob() { };
                    resetFadeBoxValueJob.Schedule(state.Dependency).Complete();
                    break;

                default:
                    Debug.Log("Error with FadeBox count.");
                    break;
            }

            renderer.color = fadeBoxAspect.fadeBoxData.ValueRO.newColor;
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
[BurstCompile]
public partial struct FadeInJob : IJobEntity
{
    public float deltaTime;
    public Color myCurrentColor;
    public void Execute(FadeBoxAspect fadeBoxAspect) => fadeBoxAspect.ReduceFadeBoxAlphaBy(deltaTime, myCurrentColor);
}
[BurstCompile]
public partial struct FadeOutJob : IJobEntity
{
    public float deltaTime;
    public Color myCurrentColor;
    public void Execute(FadeBoxAspect fadeBoxAspect) => fadeBoxAspect.IncreaseFadeBoxAlphaBy(deltaTime, myCurrentColor);
}
[BurstCompile]
public partial struct ResetFadeBoxValuesJob : IJobEntity
{
    public void Execute(FadeBoxAspect fadeBoxAspect) => fadeBoxAspect.ResetFadeBoxValues();
}
[BurstCompile]
public partial struct UpdateFadeBoxPositionJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(FadeBoxAspect fadeBoxAspect, Entity entity) => fadeBoxAspect.UpdateFadeBoxPosition(ecb, entity);
}
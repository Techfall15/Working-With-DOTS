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
        foreach (var (fadeBoxData, localTransform, entity) in SystemAPI.Query<RefRW<FadeBoxData>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            SpriteRenderer  renderer        = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            float           deltaTime       = SystemAPI.Time.DeltaTime;
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

            if (fadeBoxData.ValueRO.fadeCount > 1)
            {
                ResetFadeValuesJob resetFadeValueJob = new ResetFadeValuesJob() { };
                resetFadeValueJob.Schedule(state.Dependency).Complete();
            }
            if(fadeBoxData.ValueRO.fadeCount == 1)
            {
                MoveFadeBox moveFadeBoxJob = new MoveFadeBox()
                {
                    ecb             = ecb,
                    entityManager   = state.EntityManager,
                };
                moveFadeBoxJob.Schedule(state.Dependency).Complete();
                Camera.main.transform.position = new float3(fadeBoxData.ValueRO.newPosition.x, fadeBoxData.ValueRO.newPosition.y, -10f);
            }
            fadeOutJob.Schedule(state.Dependency).Complete();
            fadeInJob.Schedule(state.Dependency).Complete();

            renderer.color = fadeBoxData.ValueRO.newColor;
        }
        ecb.Playback(state.EntityManager);

    }


}
[BurstCompile]
public partial struct FadeInJob : IJobEntity
{
    public float deltaTime;
    public Color myCurrentColor;
    public void Execute(ref FadeBoxData fadeBoxData, Entity entity)
    {
        if (fadeBoxData.isFading == true && fadeBoxData.isFadingOut == false)
        {
            Color currentColor = myCurrentColor;
            float newAlpha = ((currentColor.a + deltaTime) < 1f) ? (currentColor.a + deltaTime) : 1f;
            Color newColor = new Color()
            {
                r = currentColor.r,
                g = currentColor.g,
                b = currentColor.b,
                a = newAlpha
            };
            fadeBoxData.newColor = newColor;
            if (newAlpha >= 1f)
            {
                fadeBoxData.isFadingOut = true;
                fadeBoxData.fadeCount++;
            }
        }
    }
}
[BurstCompile]
public partial struct FadeOutJob : IJobEntity
{
    public float deltaTime;
    public Color myCurrentColor;
    public void Execute(ref FadeBoxData fadeBoxData, Entity entity)
    {
        // If fading is enabled...
        if (fadeBoxData.isFading == true && fadeBoxData.isFadingOut == true)
        {
            Color currentColor = myCurrentColor;
            float newAlpha = ((currentColor.a - deltaTime) > 0) ? (currentColor.a - deltaTime) : 0;
            Color newColor = new Color()
            {
                r = currentColor.r,
                g = currentColor.g,
                b = currentColor.b,
                a = newAlpha
            };
            fadeBoxData.newColor = newColor;
            if (newAlpha <= 0f)
            {
                fadeBoxData.isFadingOut = false;
                fadeBoxData.fadeCount++;
            }

        }
    }
}
[BurstCompile]
public partial struct ResetFadeValuesJob : IJobEntity
{
    public void Execute(ref FadeBoxData fadeBoxData)
    {
        fadeBoxData.fadeCount   = 0;
        fadeBoxData.isFading    = false;
        fadeBoxData.isFadingOut = false;
        fadeBoxData.newColor    = new Color(0, 0, 0, 0);
    }
}
[BurstCompile]
public partial struct MoveFadeBox : IJobEntity
{
    public EntityCommandBuffer ecb;
    public EntityManager entityManager;
    
    public void Execute(in FadeBoxData fadeBoxData, Entity entity)
    {
        LocalTransform fadeBoxTransform = new LocalTransform()
        {
            Position = new float3(fadeBoxData.newPosition.x, fadeBoxData.newPosition.y, 0f),
            Rotation = Quaternion.identity,
            Scale = 1f
        };
        ecb.SetComponent<LocalTransform>(entity, fadeBoxTransform);
    }
}
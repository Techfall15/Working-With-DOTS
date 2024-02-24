using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct FadeBoxSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FadeBoxData>();
    }


    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        // For each fade box
        foreach(var(fadeBoxData, localTransform, entity) in SystemAPI.Query<RefRW<FadeBoxData>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            SpriteRenderer renderer = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            if (fadeBoxData.ValueRO.fadeCount > 1)
            {
                fadeBoxData.ValueRW.fadeCount = 0;
                fadeBoxData.ValueRW.isFading = false;
                fadeBoxData.ValueRW.isFadingOut = false;
                fadeBoxData.ValueRW.newColor = new Color(0, 0, 0, 0);
                
            }
            if(fadeBoxData.ValueRO.fadeCount == 1)
            {
                LocalTransform fadeBoxTransform = new LocalTransform()
                {
                    Position = new float3(fadeBoxData.ValueRO.newPosition.x, fadeBoxData.ValueRO.newPosition.y, 0f),
                    Rotation = Quaternion.identity,
                    Scale = 1f
                };
                ecb.SetComponent<LocalTransform>(entity, fadeBoxTransform);
                Camera.main.transform.position = new float3(fadeBoxData.ValueRO.newPosition.x, fadeBoxData.ValueRO.newPosition.y,-10f);
            }
            // If fading is enabled...
            if(fadeBoxData.ValueRO.isFading == true && fadeBoxData.ValueRO.isFadingOut == true)
            {
                Color currentColor = renderer.color;
                float newAlpha = ((currentColor.a - SystemAPI.Time.DeltaTime) > 0) ? (currentColor.a - SystemAPI.Time.DeltaTime) : 0;
                Color newColor = new Color()
                {
                    r = currentColor.r,
                    g = currentColor.g,
                    b = currentColor.b,
                    a = newAlpha
                };
                fadeBoxData.ValueRW.newColor = newColor;
                if (newAlpha <= 0f)
                {
                    fadeBoxData.ValueRW.isFadingOut = false;
                    fadeBoxData.ValueRW.fadeCount++;
                }
            }
            if(fadeBoxData.ValueRO.isFading == true && fadeBoxData.ValueRO.isFadingOut == false)
            {
                Color currentColor = renderer.color;
                float newAlpha = ((currentColor.a + SystemAPI.Time.DeltaTime) < 1f) ? (currentColor.a + SystemAPI.Time.DeltaTime) : 1f;
                Color newColor = new Color()
                {
                    r = currentColor.r,
                    g = currentColor.g,
                    b = currentColor.b,
                    a = newAlpha
                };
                fadeBoxData.ValueRW.newColor = newColor;
                if (newAlpha >= 1f)
                {
                    fadeBoxData.ValueRW.isFadingOut = true;
                    fadeBoxData.ValueRW.fadeCount++;
                }
            }

            renderer.color = fadeBoxData.ValueRO.newColor;
        }
        ecb.Playback(state.EntityManager);

    }



}
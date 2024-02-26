using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using static UnityEngine.EventSystems.EventTrigger;

public readonly partial struct FadeBoxAspect : IAspect
{

    public readonly RefRW<FadeBoxData> fadeBoxData;
    public readonly RefRO<LocalTransform> localTransform;

    public void SetNewFadeBoxPosition(float3 newPos)
    {
        if (fadeBoxData.ValueRO.fadeCount < 2) fadeBoxData.ValueRW.isFading = true;
        fadeBoxData.ValueRW.newPosition = newPos;
    }
    public void ResetFadeBoxValues()
    {
        fadeBoxData.ValueRW.fadeCount = 0;
        fadeBoxData.ValueRW.isFading = false;
        fadeBoxData.ValueRW.isFadingOut = false;
        fadeBoxData.ValueRW.newColor = new Color(0, 0, 0, 0);
    }
    public void ReduceFadeBoxAlphaBy(float deltaTime, Color myCurrentColor)
    {
        if (fadeBoxData.ValueRO.isFading == true && fadeBoxData.ValueRO.isFadingOut == false)
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
            fadeBoxData.ValueRW.newColor = newColor;
            if (newAlpha >= 1f)
            {
                fadeBoxData.ValueRW.isFadingOut = true;
                fadeBoxData.ValueRW.fadeCount++;
            }
        }
    }
    public void IncreaseFadeBoxAlphaBy(float deltaTime, Color myCurrentColor)
    {
        // If fading is enabled...
        if (fadeBoxData.ValueRO.isFading == true && fadeBoxData.ValueRO.isFadingOut == true)
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
            fadeBoxData.ValueRW.newColor = newColor;
            if (newAlpha <= 0f)
            {
                fadeBoxData.ValueRW.isFadingOut = false;
                fadeBoxData.ValueRW.fadeCount++;
            }

        }
    }
    public void UpdateFadeBoxPosition(EntityCommandBuffer ecb, Entity entity)
    {
        LocalTransform fadeBoxTransform = new LocalTransform()
        {
            Position = new float3(fadeBoxData.ValueRO.newPosition.x, fadeBoxData.ValueRO.newPosition.y, 0f),
            Rotation = Quaternion.identity,
            Scale = 1f
        };
        ecb.SetComponent<LocalTransform>(entity, fadeBoxTransform);
    }
}
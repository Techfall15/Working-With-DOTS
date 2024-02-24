using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class FadeBoxDataAuthoring : MonoBehaviour
{
    public bool isFading    = false;
    public bool isFadingOut = false;
    public float currentAlpha;
    public float fadeTime;
    public Color newColor;
    public int fadeCount = 0;
    public float3 newPosition;
    public class FadeboxDataBaker : Baker<FadeBoxDataAuthoring>
    {
        public override void Bake(FadeBoxDataAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new FadeBoxData()
            {
                currentAlpha    = authoring.currentAlpha,
                fadeTime        = authoring.fadeTime,
                newColor        = authoring.newColor,
                isFading        = authoring.isFading,
                isFadingOut     = authoring.isFadingOut,
                fadeCount       = authoring.fadeCount,
                newPosition = authoring.newPosition,
            });


        }


    }



}
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public class RotationOverTimeAuthoring : MonoBehaviour
{
    public float    rotationSpeed       = 0f;
    public bool     clockwiseRotation   = true;
    public bool     randomizeDirection  = true;
    public bool     randomizeSpeed      = true;
    public bool     canChangeDirection  = true;
    public float2   minMax              = new float2(0f, 0.5f);
    private class RotationOverTimeBaker : Baker<RotationOverTimeAuthoring> 
    {
        public override void Bake(RotationOverTimeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotationOverTimeComponentData
            {
                rotationSpeed       = authoring.rotationSpeed,
                clockwiseRotation   = authoring.clockwiseRotation,
                randomizeDirection  = authoring.randomizeDirection,
                randomizeSpeed      = authoring.randomizeSpeed,
                canChangeDirection  = authoring.canChangeDirection,
                minMax              = authoring.minMax,
            });
        }


    }
}
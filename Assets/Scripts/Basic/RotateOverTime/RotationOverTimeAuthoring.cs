using UnityEngine;
using Unity.Entities;

public class RotationOverTimeAuthoring : MonoBehaviour
{
    public float    rotationSpeed       = 0f;
    public bool     clockwiseRotation   = false;

    private class RotationOverTimeBaker : Baker<RotationOverTimeAuthoring> 
    {

        public override void Bake(RotationOverTimeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotationOverTimeComponentData
            {
                rotationSpeed       = authoring.rotationSpeed,
                clockwiseRotation   = authoring.clockwiseRotation,
            });
        }


    }
}
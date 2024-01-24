using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
public class TriRotateSpeedAuthoring : MonoBehaviour
{

    public float rotationSpeed;

    private class TriRotateSpeedBaker: Baker<TriRotateSpeedAuthoring> 
    {
        public override void Bake(TriRotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TriRotateSpeed
            {
                rotationSpeed = authoring.rotationSpeed
            });
        }
    }
}

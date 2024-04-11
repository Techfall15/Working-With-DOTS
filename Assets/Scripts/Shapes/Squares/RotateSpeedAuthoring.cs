using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;


public class RotateSpeedAuthoring : MonoBehaviour
{
    public float speed;


    private class RotateSpeedBaker: Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                speed = authoring.speed,
            });
        }
    }


}

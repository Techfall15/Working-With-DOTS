using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class SlidingSpeedAuthoring : MonoBehaviour
{

    public float slideSpeed;


    private class SlidingSpeedBaker: Baker<SlidingSpeedAuthoring>
    {

        public override void Bake(SlidingSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SlidingSpeed
            {
                slideSpeed = authoring.slideSpeed
            });
        }
    }

}

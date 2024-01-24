using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct CircleSlidingSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {

        foreach((RefRW<LocalTransform> myLocalTransform, RefRO<SlidingSpeed> slidingSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<SlidingSpeed>>())
        {
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(
                slidingSpeed.ValueRO.slideSpeed * SystemAPI.Time.DeltaTime,
                0,
                0));
        }


    }



}

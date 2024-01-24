using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial struct TriRotateSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        foreach((RefRW<LocalTransform> myLocalTransform, RefRO<TriRotateSpeed> triRotateSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<TriRotateSpeed>>())
        {
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.RotateZ(triRotateSpeed.ValueRO.rotationSpeed * SystemAPI.Time.DeltaTime);
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(0, (-1 * SystemAPI.Time.DeltaTime), 0));
        }
    }

}

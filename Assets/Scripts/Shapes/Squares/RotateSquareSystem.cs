using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;


public partial struct RotateSquareSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed )
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateZ(rotateSpeed.ValueRO.speed * SystemAPI.Time.DeltaTime);
        }
    }
}

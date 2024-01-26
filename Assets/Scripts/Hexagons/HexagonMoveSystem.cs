using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct HexagonMoveSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRW<LocalTransform> myLocalTransform, RefRO<HexagonMoveSpeed> hexagonMoveSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<HexagonMoveSpeed>>())
        {
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(
                hexagonMoveSpeed.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime,
                0,
                0));

        }
            


    }


}

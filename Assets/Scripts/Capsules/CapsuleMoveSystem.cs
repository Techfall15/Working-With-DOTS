using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial struct CapsuleMoveSystem : ISystem
{
    
    public void OnUpdate(ref SystemState state)
    {

        foreach((RefRW<LocalTransform> myLocalTransform, RefRO<CapsuleMoveSpeed> moveSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CapsuleMoveSpeed>>())
        {
            float dTime = SystemAPI.Time.DeltaTime;
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(
                moveSpeed.ValueRO.moveSpeed * dTime * -1,
                moveSpeed.ValueRO.moveSpeed * dTime,
                0));
        }



    }



}

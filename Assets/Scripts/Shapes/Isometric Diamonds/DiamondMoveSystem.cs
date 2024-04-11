using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial struct DiamondMoveSystem : ISystem
{


    public void OnUpdate(ref SystemState state) 
    { 
        foreach((RefRW<LocalTransform> myLocalTransform, RefRO<DiamondMoveSpeed> moveSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<DiamondMoveSpeed>>())
        {

            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(0, moveSpeed.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime, 0));

        }    
    }
}

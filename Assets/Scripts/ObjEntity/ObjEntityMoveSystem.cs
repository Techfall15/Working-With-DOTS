using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct ObjEntityMoveSystem : ISystem
{
    

    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRW<LocalTransform> myLocalTransform, RefRO<InputData> myInputData, RefRO<ObjEntitySpeed> myObjEntitySpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<InputData>, RefRO<ObjEntitySpeed>>())
        {
            var move = myInputData.ValueRO.move;
            var speed = myObjEntitySpeed.ValueRO.objMoveSpeed;
            var deltaTime = SystemAPI.Time.DeltaTime;

            if (move.x == 0) continue;
            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(
                move.x * speed * deltaTime,
                0,
                0));
        };

    }

}

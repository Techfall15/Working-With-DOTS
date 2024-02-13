using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public readonly partial struct MoveWithKeyboardAspect : IAspect
{
    public readonly RefRO<MoveWithKeyboardData> moveWithKeyboardData;
    public readonly RefRW<LocalTransform>       localTransform;
    public readonly RefRO<InputData>            inputData;

    public void MoveEntity(float deltaTime)
    {
        var moveSpeed = moveWithKeyboardData.ValueRO.moveSpeed;
        var move = inputData.ValueRO.move;

        localTransform.ValueRW = localTransform.ValueRO.Translate(new float3(
            move.x * moveSpeed * deltaTime,
            move.y * moveSpeed * deltaTime,
            0));
    }

    public bool IsMovingCheck()
    {
        float2 moveData = inputData.ValueRO.move;

        if (moveData.x == 0 && moveData.y == 0) { return true; }
        else return false;
    }
}
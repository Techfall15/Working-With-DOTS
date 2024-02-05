using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public readonly partial struct PlayerAspect : IAspect
{
    public readonly RefRO<PlayerMoveSpeed>  playerMoveSpeed;
    public readonly RefRW<LocalTransform>   localTransform;
    public readonly RefRO<InputData>        inputData;

    public void MovePlayer(float deltaTime)
    {
        var moveSpeed   = playerMoveSpeed.ValueRO.playerSpeed;
        var move        = inputData.ValueRO.move;

        localTransform.ValueRW = localTransform.ValueRO.Translate(new float3(
            move.x * moveSpeed * deltaTime,
            move.y * moveSpeed * deltaTime,
            0));
    }
}

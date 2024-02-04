using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct PlayerMoveSystem : ISystem
{

    public void OnUpdate(ref SystemState state) 
    {
        
        foreach((RefRW<LocalTransform> myLocalTransform, RefRO<InputData> myInputData, RefRO<PlayerMoveSpeed> playerMoveSpeed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<InputData>, RefRO<PlayerMoveSpeed>>())
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var moveSpeed = playerMoveSpeed.ValueRO.playerSpeed;
            var move = myInputData.ValueRO.move;

            myLocalTransform.ValueRW = myLocalTransform.ValueRO.Translate(new float3(
                move.x * moveSpeed * deltaTime,
                move.y * moveSpeed * deltaTime,
                0));
        };

    }
}

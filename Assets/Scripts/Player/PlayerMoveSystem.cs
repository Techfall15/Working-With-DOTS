using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct PlayerMoveSystem : ISystem
{

    public void OnUpdate(ref SystemState state) 
    {
        PlayerMoveJob playerMoveJob = new PlayerMoveJob { deltaTime = SystemAPI.Time.DeltaTime };
        playerMoveJob.Schedule();
    }


}

public partial struct PlayerMoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(PlayerAspect playerAspect) => playerAspect.MovePlayer(deltaTime);
}

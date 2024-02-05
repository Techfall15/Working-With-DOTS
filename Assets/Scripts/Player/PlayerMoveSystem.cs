using Unity.Entities;
using Unity.Burst;

public partial struct PlayerMoveSystem : ISystem
{

    public void OnUpdate(ref SystemState state) 
    {
        PlayerMoveJob playerMoveJob = new PlayerMoveJob { deltaTime = SystemAPI.Time.DeltaTime };
        playerMoveJob.Schedule();
    }


}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(PlayerAspect playerAspect) => playerAspect.MovePlayer(deltaTime);
}

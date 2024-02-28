using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;


public partial struct PlayerMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InputData>();
    }
    public void OnUpdate(ref SystemState state) 
    {
        //PlayerMoveJob playerMoveJob = new PlayerMoveJob { deltaTime = SystemAPI.Time.DeltaTime };
        //playerMoveJob.Schedule();
        
        foreach(var (inputData, localTransform) in SystemAPI.Query<RefRO<InputData>, RefRO<LocalTransform>>().WithAll<PlayerTagComponent>())
        {
            Vector2 sceneMousePos = inputData.ValueRO.mousePos;
            
            Ray ray = Camera.main.ScreenPointToRay(sceneMousePos);
            if (Input.GetMouseButtonDown(0))
            {
                var distanceAway = Vector2.Distance(ray.origin, (Vector3)localTransform.ValueRO.Position);
                if (distanceAway < 0.5f) Debug.Log("Player should have been clicked on!");
            }
        }
    }


}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(PlayerAspect playerAspect) => playerAspect.MovePlayer(deltaTime);
}

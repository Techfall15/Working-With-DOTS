using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public partial struct PlayerOnClickHandler : ISystem
{
        public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerOnClickData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);


        foreach (var (inputData,playerClickData,localTransform, entity) in SystemAPI.Query<RefRO<InputData>,RefRW<PlayerOnClickData>, RefRO<LocalTransform>>().WithAll<PlayerTagComponent>().WithEntityAccess())
        {
            Vector2 sceneMousePos = inputData.ValueRO.mousePos;

            Ray ray = Camera.main.ScreenPointToRay(sceneMousePos);
            if (Input.GetMouseButtonDown(0))
            {
                var distanceAway = Vector2.Distance(ray.origin, (Vector3)localTransform.ValueRO.Position);
                if (distanceAway < 0.5f) playerClickData.ValueRW.hasBeenClickedOn = true;
            }

            if(playerClickData.ValueRO.hasBeenClickedOn == true)
            {
                Entity newQuestionMarkEntity = ecb.Instantiate(playerClickData.ValueRO.entityToSpawn);
                float3 playerPos = localTransform.ValueRO.Position;
                float3 spawnPos = new float3(playerPos.x + playerClickData.ValueRO.spawnPos.x, playerPos.y + playerClickData.ValueRO.spawnPos.y, 0);

                LocalTransform newQMarkTransform = new LocalTransform()
                {
                    Position = spawnPos,
                    Rotation = Quaternion.identity,
                    Scale = 1f
                };
                ecb.SetComponent<LocalTransform>(newQuestionMarkEntity, newQMarkTransform);
                playerClickData.ValueRW.hasBeenClickedOn = false;
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }


}

using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public partial struct FollowPlayersHeadSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerInteractData>();
    }



    public void OnUpdate(ref SystemState state)
    {

        foreach( var (followPlayerHeadTag, localTransform) in SystemAPI.Query<RefRO<FollowPlayerHeadTag>, RefRW<LocalTransform>>())
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
            var playerTransform = SystemAPI.GetComponentRO<LocalTransform>(playerEntity);


            localTransform.ValueRW.Position = playerTransform.ValueRO.Position + new float3(0, 1.25f, 0);

        }




    }



}
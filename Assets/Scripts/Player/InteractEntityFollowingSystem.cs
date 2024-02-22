using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public partial struct InteractEntityFollowingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractEntityTagComponent>();
    }




    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        float3 playerPos = new float3(0, 0, 0);
        foreach(var(playerLocalTransform, playerInteractData) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerInteractData>>())
        {
            playerPos = playerLocalTransform.ValueRO.Position;
        }
        foreach(var(interactLocalTransform, interactTagComponent, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<InteractEntityTagComponent>>().WithEntityAccess())
        {
            LocalTransform newEntityPos = new LocalTransform()
            {
                Position = new float3(playerPos.x, playerPos.y + 1.1f, playerPos.z),
                Rotation = Quaternion.identity,
                Scale = 1f,
            };
            ecb.SetComponent<LocalTransform>(entity, newEntityPos);
        }
        ecb.Playback(state.EntityManager);
    }





}

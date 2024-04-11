using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct PlayerRotationHandler : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTagComponent>();
    }
    public void OnUpdate(ref SystemState state)
    {

/*
        foreach(var(localTransform, playerTag) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerTagComponent>>())
        {
            if (localTransform.ValueRO.Rotation == Quaternion.Euler(0, 0, 0)) continue;
            else localTransform.ValueRW.Rotation = Quaternion.Euler(0, 0, 0);
        }*/



    }


}

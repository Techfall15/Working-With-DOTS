using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial struct FillSystem : ISystem
{
    

    public void OnUpdate(ref SystemState state)
    {
        
        foreach(var (transform,fillData,entity)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<FillData>>().WithEntityAccess())
        {
            
            var position = transform.ValueRO.Position;
            position.x = fillData.ValueRO.fillPosition.x;
            float3 newScale = new float3(fillData.ValueRO.fillScale.x, fillData.ValueRO.fillScale.y, fillData.ValueRO.fillScale.z);
            PostTransformMatrix newLTW = new PostTransformMatrix { Value = float4x4.TRS(position, transform.ValueRO.Rotation, newScale) };
            state.EntityManager.SetComponentData(entity, newLTW);

            transform.ValueRW = transform.ValueRO.WithPosition(position);
            
            
            
        }

    }
}

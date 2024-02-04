using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial struct FillSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FillData>();
    }
    public void OnUpdate(ref SystemState state)
    {
        UpdateFillData(state);
        foreach(var (transform,fillData,entity)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<FillData>>().WithEntityAccess())
        {
            var position = transform.ValueRO.Position;
            position.x = fillData.ValueRO.fillPosition.x;

            float3 newScale = new float3(fillData.ValueRO.fillScale.x, fillData.ValueRO.fillScale.y, fillData.ValueRO.fillScale.z);
            PostTransformMatrix newPTM = new PostTransformMatrix { Value = float4x4.TRS(position, transform.ValueRO.Rotation, newScale) };
            state.EntityManager.SetComponentData(entity, newPTM);


            transform.ValueRW = transform.ValueRO.WithPosition(position);
        }

    }
    private void UpdateFillData(SystemState state)
    {
        foreach (var (transform, healthBarData, entity)
            in SystemAPI.Query<RefRO<LocalTransform>, RefRO<HealthBarData>>().WithEntityAccess())
        {
            if (state.EntityManager.HasBuffer<Child>(entity) == false) continue;                            // Check for child buffer
            Entity childEntity  = state.EntityManager.GetBuffer<Child>(entity).ElementAt(0).Value;
            Entity fillEntity   = state.EntityManager.GetBuffer<Child>(childEntity).ElementAt(1).Value;     // Get a reference to child entity

            if (state.EntityManager.HasComponent<FillData>(fillEntity) == false) continue;                  // Check if child has FillData
            float newScale = healthBarData.ValueRO.healthLeft / healthBarData.ValueRO.maxHealth;            // Get new scale based on percent health left
            float newXPos = (1f - newScale) / 4f;                                                           // Get new position based on xScale

            state.EntityManager.SetComponentData(fillEntity, new FillData                                   // Set new fill data
            {
                fillPosition = new Vector3(-newXPos, 0, 0),
                fillScale = new Vector3(newScale, 0.1f, 1)
            });

        }
    }
}
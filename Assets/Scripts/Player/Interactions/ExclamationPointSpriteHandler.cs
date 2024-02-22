using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public partial struct ExclamationPointSpriteHandler : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ExclamationPointData>();
    }


    public void OnUpdate(ref SystemState state)
    {

        foreach(var(exclamationPointData, entity) in SystemAPI.Query<RefRW<ExclamationPointData>>().WithEntityAccess())
        {
            SpriteRenderer              renderer    = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            ExclamationPointSpriteData  spriteData  = state.EntityManager.GetComponentObject<ExclamationPointSpriteData>(entity);

            if(exclamationPointData.ValueRO.timeBetweenFrames > 0)
            {
                exclamationPointData.ValueRW.timeBetweenFrames -= SystemAPI.Time.DeltaTime;
            }
            else
            {
                exclamationPointData.ValueRW.currentSpriteIndex = (exclamationPointData.ValueRO.currentSpriteIndex < spriteData.spriteList.Length - 1) ? exclamationPointData.ValueRO.currentSpriteIndex + 1 : 0;
                exclamationPointData.ValueRW.timeBetweenFrames  = exclamationPointData.ValueRO.maxTimeBetweenFrames;
            }

            renderer.sprite = spriteData.spriteList[exclamationPointData.ValueRW.currentSpriteIndex];
        }





    }


}
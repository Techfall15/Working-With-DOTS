using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;


public partial struct BonsaiTreeSpriteHandler : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BonsaiTreeData>();
    }

    public void OnUpdate(ref SystemState state)
    {

        foreach(var (bonsaiTreeData, entity) in SystemAPI.Query<RefRW<BonsaiTreeData>>().WithEntityAccess())
        {

            if (bonsaiTreeData.ValueRO.isAnimating == false) continue;

            BonsaiTreeSpriteData    spriteData  = state.EntityManager.GetComponentObject<BonsaiTreeSpriteData>(entity);
            SpriteRenderer          renderer    = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            int                     spriteIndex = bonsaiTreeData.ValueRO.currentSpriteIndex;
            
            renderer.sprite = spriteData.spriteList[spriteIndex];

            bonsaiTreeData.ValueRW.timeBetweenFrames = bonsaiTreeData.ValueRO.timeBetweenFrames - SystemAPI.Time.DeltaTime;
            if(bonsaiTreeData.ValueRO.timeBetweenFrames <= 0)
            {
                bonsaiTreeData.ValueRW.currentSpriteIndex   = (bonsaiTreeData.ValueRO.currentSpriteIndex == spriteData.spriteList.Length - 1) ? 0 : bonsaiTreeData.ValueRO.currentSpriteIndex + 1;
                bonsaiTreeData.ValueRW.timeBetweenFrames    = bonsaiTreeData.ValueRO.maxTimeBetweenFrames;
            }
        }



    }



}
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public partial struct GoldCoinSpriteHandler : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GoldCoinData>();
    }

    public void OnUpdate(ref SystemState state)
    {

        
        foreach(var (goldCoinData, entity) in SystemAPI.Query<RefRW<GoldCoinData>>().WithEntityAccess())
        {

            if (goldCoinData.ValueRO.isAnimating == false) continue;

            SpriteRenderer renderer = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            GoldCoinSpriteData spriteData = state.EntityManager.GetComponentObject<GoldCoinSpriteData>(entity);
            int spriteIndex = goldCoinData.ValueRO.currentSpriteIndex;

            renderer.sprite = spriteData.spriteList[spriteIndex];

            goldCoinData.ValueRW.timeBetweenFrames = goldCoinData.ValueRO.timeBetweenFrames - SystemAPI.Time.DeltaTime;
            if(goldCoinData.ValueRO.timeBetweenFrames <= 0)
            {
                goldCoinData.ValueRW.timeBetweenFrames = goldCoinData.ValueRO.maxTimeBetweenFrames;
                goldCoinData.ValueRW.currentSpriteIndex = (goldCoinData.ValueRO.currentSpriteIndex == spriteData.spriteList.Length - 1) ? 0 : goldCoinData.ValueRO.currentSpriteIndex + 1;
            }
        }




    }


}
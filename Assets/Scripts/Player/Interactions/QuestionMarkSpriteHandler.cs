using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct QuestMarkSpriteHandler : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<QuestionMarkData>();
    }

    public void OnUpdate(ref SystemState state)
    {

        foreach (var (questionMarkData, entity) in SystemAPI.Query<RefRW<QuestionMarkData>>().WithEntityAccess())
        {
            SpriteRenderer renderer = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            QuestionMarkSpriteData spriteData = state.EntityManager.GetComponentObject<QuestionMarkSpriteData>(entity);

            if (questionMarkData.ValueRO.timeBetweenFrames > 0)
            {
                questionMarkData.ValueRW.timeBetweenFrames -= SystemAPI.Time.DeltaTime;
            }
            else
            {
                questionMarkData.ValueRW.currentSpriteIndex = (questionMarkData.ValueRO.currentSpriteIndex < spriteData.spriteList.Length - 1) ? questionMarkData.ValueRO.currentSpriteIndex + 1 : 0;
                questionMarkData.ValueRW.timeBetweenFrames = questionMarkData.ValueRO.maxTimeBetweenFrames;
            }

            renderer.sprite = spriteData.spriteList[questionMarkData.ValueRW.currentSpriteIndex];
        }



    }

}
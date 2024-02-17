using UnityEngine;
using Unity.Entities;
using UnityEngine.U2D;
using System.Collections.Generic;


public partial struct TreasureChestOpenCloseSystem : ISystem
{


    public void OnUpdate(ref SystemState state)
    {

        
        foreach((RefRO<InputData> inputData, RefRW<TreasureChestData> chestData, Entity entity)
            in SystemAPI.Query<RefRO<InputData>, RefRW<TreasureChestData>>().WithEntityAccess())
        {

            if(inputData.ValueRO.openChest)
            {
                chestData.ValueRW.isOpen = !chestData.ValueRO.isOpen;
                chestData.ValueRW.currentSpriteIndex = (chestData.ValueRO.isOpen == true) ? 1 : 0;
            }

            SpriteRenderer renderer             = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            TreasureChestSpriteData spriteData  = state.EntityManager.GetComponentObject<TreasureChestSpriteData>(entity);

            renderer.sprite = spriteData.spriteList[chestData.ValueRO.currentSpriteIndex];

        }



    }



}
public class TreasureChestSpriteData : IComponentData
{
    public Sprite[] spriteList;
}
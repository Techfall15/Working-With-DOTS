using UnityEngine;
using Unity.Entities;


public partial struct WoodenDoorSpriteHandler : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<WoodenDoorSpriteData>();
    }


    public void OnUpdate(ref SystemState state)
    {

        foreach(var(woodenDoorData, entity) in SystemAPI.Query<RefRW<WoodenDoorData>>().WithEntityAccess())
        {
            woodenDoorData.ValueRW.currentSpriteIndex = (woodenDoorData.ValueRO.isOpen == true) ? 2 : 0;

            SpriteRenderer          renderer    = state.EntityManager.GetComponentObject<SpriteRenderer>(entity);
            WoodenDoorSpriteData    spriteData  = state.EntityManager.GetComponentObject<WoodenDoorSpriteData>(entity);

            renderer.sprite = spriteData.spriteList[woodenDoorData.ValueRO.currentSpriteIndex];
        }


    }


}

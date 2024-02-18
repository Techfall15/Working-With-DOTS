using UnityEngine;
using Unity.Entities;


public partial struct TreasureChestOpenCloseSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<TreasureChestData>();
    }

    public void OnUpdate(ref SystemState state)
    {

        
    }
}
public class TreasureChestSpriteData : IComponentData
{
    public Sprite[] spriteList;
}
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
public partial struct TreasureChestData : IComponentData
{

    public bool     isOpen;
    public int      currentSpriteIndex;

}
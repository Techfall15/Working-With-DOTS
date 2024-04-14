using UnityEngine;
using Unity.Entities;

public struct SpriteColorEditComponentData : IComponentData
{
    public Color spriteColor;
    public bool randomizeColor;
    public bool isColorSet;
}
